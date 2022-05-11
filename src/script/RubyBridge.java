package script;

import java.io.PrintWriter;
import java.io.StringWriter;

import org.jruby.Ruby;
import org.jruby.embed.ScriptingContainer;

import engine.Application;
import engine.data.ProjectData;
import engine.tools.Printer;
import engine.tools.Toolkit;
import engine.tools.maths.Maths;

/**
 * @autor Luca Marpeau - 13 mars 2022
 * This following file is a part of JavaVisualNovel project. 
 * 
 * @description
 * This is the main class for JRuby compiler.
 * This class make a bridge between Ruby Script and Java Engine.
 * 
*/
public class RubyBridge{
	
	public static final String RUBY_CONTENT = "assets/ruby/";
	public static final String[] RUBY_EXTENSIONS = {".rb", ".ruby"};
	
	public static final String RUBY_OUTPUT = "assets/logs/script.log";
	
	public boolean isCoreRubyInit = false;
	
	private ScriptLoader loader;
	private String script;
	
	private Ruby ruby;
	
	/* Constructor */
	public RubyBridge(ProjectData data) {
		loader = new ScriptLoader(data);
		ruby = Ruby.newInstance();
	}
	
	/**
	 * This is the main method from Ruby Bridge.
	 * Load all ruby script and execute it using ruby compiler.
	 * Use error catching to prevent fatal error.
	 * 
	 */
	public void run() {
		script = loadFiles();
		System.out.println("INFO: Compiling scripts...");
		
		loader.writeOuputFile(script);
		try {
			evalScript(script);
			
		} catch (Exception e) {
			catchScriptError(e);
		}
	}
	
	/**
	 * When application is stopped. 
	 * Execute the 'end' event to Ruby.
	 */
	public void stop() {
		method("end");
		
		System.out.println("\n--------------------------------\n"  + "\nSUCCESS: Compiling is finished!");	
	}
	
	/**
	 * Execute and compiling String code.
	 * 
	 */
	private void evalScript(String scipt) throws Exception {
		System.out.println("\n------------ OUTPUT ------------\n");
		
		ruby.evalScriptlet(scipt);
		method("begin");
	}
	
	/**
	 * Call a method from ruby.
	 * Use a String as the method's name.
	 * 
	 */
	public void method(String name) {
		ruby.getArray().callMethod(name);
	}
	
	/**
	 * Load all files using the script loader.
	 * Use the default ruby extension.
	 * Return code as String.
	 * 
	 */
	public String loadFiles() {
		return loader.readAllFiles(RUBY_EXTENSIONS);
	}
	
	/**
	 * Load all files using the script loader.
	 * Use custom extension filter.
	 * Return code as String.
	 * 
	 */
	public String loadFiles(String[] extensions) {
		return loader.readAllFiles(extensions);
	}
	
	/**
	 * Execute when an error is catching while script's execution.
	 * 
	 */
	private void catchScriptError(Exception error) {
		StringWriter err = new StringWriter();
		err.append("\n------- SIMPLE TRACEBACK -------\n");
		err.append(createTraceback(error));
		err.append("\n\n-------- FULL TRACEBACK --------\n");
		
		error.printStackTrace(new PrintWriter(err));

		err.append("\nFATAL ERROR");
		
		System.err.println(err.toString());
		System.err.println("\n--------------------------------");

		//Application.CURRENT_APP.stop(-1);
	}
	
	/**
	 * Generate an error traceback using an error message.
	 * It reset to the correct line from the script.
	 * 
	 */
	private String createTraceback(Exception error) {
		String content = error.getMessage();
		String[] location = null;
		
		StringWriter string = new StringWriter();
		error.printStackTrace(new PrintWriter(string));
		
		// Path to the error
		location = string.toString().trim().split("at");
		location[0] = "";
		
		// Loop thought error's path
		for(int i = 1; i < location.length; i++) {
			String line = location[i].trim();
			if(!line.isEmpty() && line.contains(":")) {
				String name = line.split("[(]")[0];
				line = line.split(":")[1];
				
				String[] fileData;
				try {
					fileData = loader.getFileFromScript(Toolkit.getAllDigits(line)).split(";");
					content += "\n\tAt line " + fileData[1] + " in " + name + " -> '" + fileData[0] + "'";
				} catch (Exception e) {
				}
				
			}
		}
		
		return content;
	}

	/**
	 * Simple static script compiler.
	 * Use a string to execute simple and fast ruby script.
	 * Cannot call from Java to Ruby, only call Ruby to Java.
	 * 
	 */
	public static void RunRubyString(String s) {
		ScriptingContainer container = new ScriptingContainer();
		container.runScriptlet(s);
	}
	
	///// GETTER
	public ScriptLoader getLoader() {return loader;}
	
	///// SETTER
	public void setLoader(ScriptLoader loader) {this.loader = loader;}
}
