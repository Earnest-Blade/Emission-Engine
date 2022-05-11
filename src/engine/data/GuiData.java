package engine.data;

import java.awt.Toolkit;

import org.lwjgl.glfw.GLFW;
import org.lwjgl.system.MemoryUtil;

import engine.Application;
import engine.graphics.window.Window;
import engine.loaders.ResourceLoader;

/**
 * @autor Luca Marpeau - 14 mars 2022
 * This following file is a part of JavaVisualNovel project. 
 * 
 * @description
 * GuiData object contains all informations and variables about graphics, interface and window loading.
 * All of those variables are defined in "gui.rb" from project's script folder.
 * 
*/
public class GuiData {
	
	/**
	 * Define application's name.
	 * Default value = "Window"
	 */
	public String application_name = "Window";
	
	/**
	 * Define Window width.
	 * Default value = 1920
	 */
	public int window_width = 1920;
	
	/**
	 * Define Window height.
	 * Default value = 1080
	 */
	public int window_height = 1080;
	
	/**
	 * Define Window's x location when the window is create.
	 * Default value = Center To Screen
	 */
	public int location_x = Toolkit.getDefaultToolkit().getScreenSize().width / 2 - window_width / 2;
	
	/**
	 * Define Window's y location when the window is create.
	 * Default value = Center To Screen
	 */
	public int location_y = Toolkit.getDefaultToolkit().getScreenSize().height / 2 - window_height / 2;
	
	/**
	 * Define the path to the window/application icon.
	 * Default value = "icon.png"
	 */
	public String application_icon = "icon.png";
	
	/**
	 * Define if the window will always be on top of other applications.
	 * Default value = false
	 */
	public boolean is_always_on_top = false;
	
	/**
	 * Define if the FPS will be display on the upper right corner of the screen.
	 * Default value = false
	 */
	public boolean show_fps = false;
	
	/**
	 * Define how much the fps can be.
	 * Default value = 60
	 */
	public int fps_cap = 60;
	
	/*
	 * Define max amount of layers.
	 * Default value = 5
	 */
	public int layer_max = 5;
	
	/*
	 * Define minimum amount of layers.
	 * Default value = -5
	 */
	public int layer_min = -5;
	
	/* 
	 * Constructor
	 */
	public GuiData() {}
	
	/**
	 * This method apply all variables to the application components.
	 * The Window must be create, if it's not, an error message will be sent 
	 * and variables will not be applied.
	 * 
	 */
	public void flush() {
		// if the window is create.
		
		if(Window.getWindowID() != MemoryUtil.NULL) {
			long window = Window.getWindowID(); // get window id
			GLFW.glfwSetWindowSize(window, window_width, window_height);
			GLFW.glfwSetWindowPos(window, location_x, location_y); 
			GLFW.glfwSetWindowTitle(window, application_name);
			
			if(!application_icon.isEmpty())
				GLFW.glfwSetWindowIcon(window, ResourceLoader.loadIcon(application_icon));
		}
		// When window isn't create.
		else {
			System.err.println("ERROR; Trying to change window paramters while window isn't load!");
		}
		
	}
	
}
