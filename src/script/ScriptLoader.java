

package script;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.stream.Collectors;
import java.util.stream.Stream;

import engine.data.ProjectData;
import engine.tools.Printer;

/**
 * @autor Luca Marpeau - 13 mars 2022
 * This following file is a part of JavaVisualNovel project. 
 * 
 * @description
 * This class is an IO Script class.
 * It load and organize the entire project script and all engine's ruby script (Warpers, Class, Functions...)
*/
public class ScriptLoader {

	private String[] scriptPath;
	private String content;

	/* Constructor */
	public ScriptLoader(ProjectData data) {
		scriptPath = new String[data.getIncludePaths().length];
		for (int i = 0; i < scriptPath.length; i++)
			scriptPath[i] = data.getIncludePaths()[i].getScriptsDirectory();
	}

	/**
	 * This method read all files from all script files.
	 * It use 'readAllEngineFiles()' to get all files and use 'getAllScriptFiles()'
	 * to read all files content.
	 * It take all authorize extensions and return files content as a String.
	 * 
	 */
	public String readAllFiles(String[] extensions) {
		content = "";
		
		content += readAllEngineFiles();
		
		File[] scriptsFiles = getAllScriptFiles(extensions);
		for(int i = 0; i < scriptsFiles.length; i++) {
			content += readFile(scriptsFiles[i]);
		}

		return content;
	}
	
	private String readFile(File file) {
		String contentString = "";
		
		contentString += createFileRubyHeader(file);
		
		System.out.println("INFO: Reading " + file.getAbsolutePath() + " ...");
		FileInputStream input;
		try {
			input = new FileInputStream(file);
			StringBuilder content = new StringBuilder();
	        BufferedReader br = new BufferedReader(new InputStreamReader(input));

	        String line;

	        while ((line = br.readLine()) != null) {
	            content.append(line + System.lineSeparator());
	        }
	        
	        contentString += content.toString();
	        
		} catch (IOException e) {
			System.err.println("ERROR: Cannot read " + file.getAbsolutePath() + " script file!");
			e.printStackTrace();
		}
		
        return contentString;
	}
	
	public File[] getAllScriptFiles(String[] extensions) {
		
		ArrayList<File> files = new ArrayList<File>();
		for(int i = 0; i < scriptPath.length; i++) {
			try {
				Files.walk(Paths.get(scriptPath[i])).forEach(path -> {
				    for(String s : extensions) {
				    	if(path.toFile().getName().contains(s)) {
				    		orderFileInList(files, path);
				    	}
				    }
				});
				
			} catch (IOException e) {
				System.err.println("ERROR: Cannot walk through the directory " + scriptPath[i] + "!");
				e.printStackTrace();
			}
		}

		File[] array = new File[files.size()];
		array = files.toArray(array);
		return array;
	}
	
	public void writeOuputFile(String output) {
		try {
			File f = new File(RubyBridge.RUBY_OUTPUT);
			if(f.exists()) {
				BufferedWriter writer = new BufferedWriter(new FileWriter(RubyBridge.RUBY_OUTPUT));
				writer.write(output);
				writer.close();
			}
			else {
				f.createNewFile();
				writeOuputFile(output);
			}
			
		} catch (IOException e) { }
	}
	
	private String readAllEngineFiles() {
		String content = "";
		ArrayList<File> files = new ArrayList<File>();
		
		try {
			Files.walk(Paths.get(RubyBridge.RUBY_CONTENT)).forEach(path -> {
			    for(String s : RubyBridge.RUBY_EXTENSIONS) {
			    	if(path.toFile().getName().contains(s)) {
			    		orderFileInList(files, path);
			    	}
			    }
			});
			
		} catch (IOException e) {
			System.err.println("ERROR: Cannot walk through the directory " + RubyBridge.RUBY_CONTENT + "!");
			e.printStackTrace();
		}

		for(File file : files) {
			content += readFile(file);
		}
		
		return content;
	}
	
	private ArrayList<File> orderFileInList(ArrayList<File> files, Path path){
		
		// Check 'init_offset' parameter
		String argumentValue = getRubyFileParameter(path, "do_not_import");
		if(argumentValue == null) {
			// Check 'init_offset' parameter
			argumentValue = getRubyFileParameter(path, "init_offset");
			if(argumentValue != null) {
				int index = Integer.parseInt(argumentValue);
				if(index >= files.size()) {
					files.add(path.toFile());
				}
				else {
					files.add(index, path.toFile());
				}
			}
			else {
				files.add(path.toFile());
			}
		}
		
		return files;
	}
	
	private String createFileRubyHeader(File f) {
		return System.lineSeparator() + "#path=" + f.getPath() + 
			System.lineSeparator() +"#filename=" + f.getName() + System.lineSeparator();
	}
	
	public static String getRubyFileParameter(Path path, String name) {
		try {
			Scanner scanner = new Scanner(path.toFile());
			while(scanner.hasNextLine()) {
				String line = scanner.nextLine();
				if(line.contains("#" + name)) {
					if(line.split("=").length >= 2)
						return line.split("=")[1];
					else
						return "";
				}
			}
			
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}
		return null;
	}
	
	public String getFileFromScript(int line)  throws Exception{
		String[] s = content.split(System.lineSeparator());
		
		int index = line;
		while(!s[index].startsWith("#filename=")) {
			index--;
		}
		s[index] = s[index].split("=")[1];
		
		return s[index] + ";" + (Math.abs(index - line + 2));
	}
	
	public String[] getScriptPath() {return scriptPath;}

}
