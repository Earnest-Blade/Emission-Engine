package engine;

import engine.data.GuiData;
import engine.data.ProjectData;
import engine.graphics.window.Window;
import project.loader.ProjectLoader;
import script.RubyBridge;

public class Application{
	
	public static Application CURRENT_APP;
	
	private boolean isDebug = true;
	
	private RubyBridge ruby;
	private Window window;
	
	private ProjectData projectData;
	private GuiData guiData;
	
	/* Constructor */
	public Application() {
		guiData = new GuiData();
		window = new Window();
	} 
	
	/**
	 * This method is call when the application is loaded.
	 * It start all engine's main components.
	 */
	public void start() {
		// Start the window and his thread.
		window.startThread();
		
		// Wait for window to be started so the window can be change.
		while(!window.isVisible()) {System.out.print("");}
		
		// Start Ruby Script
		ruby.run();
	}
	
	/**
	 * This method stop the application.
	 */
	public void stop(int status) {
		if(status != -1)
			ruby.stop();
		
		System.out.println("INFO: Stopping application!");
		Window.destroyWindow();
		
		Logs.LogConsole();
		System.exit(status);
	}
	
	///// GETTER
	
	public boolean isDebug() {return isDebug;}
	public Window getWindow() {return window;}
	public RubyBridge getApplicationRuby() {return ruby; }
	public GuiData getApplicationGUIData() {return guiData;}
	public ProjectData getApplicationData() {return projectData;}
	
	///// SETTER
	
	public void setProjectData(ProjectData data) {
		this.projectData = data;
		this.ruby = new RubyBridge(data);
	}
	
	/**
	 * Load and Start application
	 */
	public static void main(String[] args) {
		Application.CURRENT_APP = ProjectLoader.generateProject("project001");
		Application.CURRENT_APP.start();
	}
}
