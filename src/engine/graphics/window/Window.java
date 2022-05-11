package engine.graphics.window;

import java.nio.DoubleBuffer;
import java.nio.IntBuffer;

import org.joml.Vector2f;
import org.lwjgl.BufferUtils;
import org.lwjgl.glfw.GLFW;
import org.lwjgl.opengl.GL;
import org.lwjgl.opengl.GL11;
import org.lwjgl.system.MemoryStack;
import org.lwjgl.system.MemoryUtil;

import engine.Application;
import engine.Input;
import engine.graphics.displayable.DisplayText;
import engine.graphics.shaders.StaticShader;
import engine.loaders.GraphicsLoader;
import engine.loaders.ResourceLoader;
import engine.tools.maths.Anchor;
import engine.tools.maths.Maths;

public class Window implements Runnable{
	
	private static long window = -1;
	
	private StaticShader shader;
	private GraphicsLoader loader;
	private boolean isVisible = false;
	
	private Thread graphicsThread;
	
	/* Constructor */
	public Window() {
		graphicsThread = new Thread(this, "GFX-Thread");
	}
	
	public void createWindow() {
		// Initialize GLFW
		if(!GLFW.glfwInit()) throw new IllegalStateException();
		
		GLFW.glfwDefaultWindowHints();
		GLFW.glfwWindowHint(GLFW.GLFW_VISIBLE, GLFW.GLFW_FALSE); // the window will stay hidden after creation
		GLFW.glfwWindowHint(GLFW.GLFW_RESIZABLE, GLFW.GLFW_TRUE); // the window will be resizable
		
		window = GLFW.glfwCreateWindow(
				Application.CURRENT_APP.getApplicationGUIData().window_width, 
				Application.CURRENT_APP.getApplicationGUIData().window_height, 
				Application.CURRENT_APP.getApplicationGUIData().application_name, MemoryUtil.NULL, MemoryUtil.NULL);
		
		if (window == MemoryUtil.NULL)
			throw new RuntimeException("ERROR: Failed to create the GLFW window");

		GLFW.glfwSetWindowPos(window, Application.CURRENT_APP.getApplicationGUIData().location_x, Application.CURRENT_APP.getApplicationGUIData().location_y);
		GLFW.glfwMakeContextCurrent(window);
		GLFW.glfwSwapInterval(0);
		GL.createCapabilities();
		
		new Renderer();
		Input.init();
		loader = new GraphicsLoader();
		shader = new StaticShader();
	
		System.out.println("SUCCESS: Window is successfuly created!");
	}
	
	public void startThread() 
	{
		System.out.println("SUCCESS: Graphic Thread is running!");
		graphicsThread.start();
	}

	@Override
	public void run() {
		createWindow();
		
		show();
		
		shader.start();
		shader.newUniform("projection", Maths.createProjectionMatrix(90, 100, 0));
		shader.stop();
		
		double currentTime, lastTime = 0;
		while(!shouldClose()) {
			
			currentTime = GLFW.glfwGetTime(); 

			if(currentTime - lastTime >= 1.0 / Application.CURRENT_APP.getApplicationGUIData().fps_cap) 
			{
				lastTime = currentTime;
				
				Renderer.clear();
				Renderer.loadQueue();
				shader.start();
				
				// TODO : Update
				Input.update();
				
				// TODO : Render
				Renderer.renderQueue(shader);
				
				// TODO : Components Update
				
				shader.stop();
				
				GLFW.glfwSwapBuffers(window);
			}
		}
		
		Application.CURRENT_APP.stop(1);
	}
	
	public static void show() {
		GLFW.glfwShowWindow(window);
		Application.CURRENT_APP.getWindow().isVisible = true;
	}
	
	public static void hide() {
		GLFW.glfwHideWindow(window);
		Application.CURRENT_APP.getWindow().isVisible = false;
	}
	
	public static void destroyWindow() {
		System.out.println("INFO: Destroying window!");
		GraphicsLoader.getLoader().clear();
		GLFW.glfwDestroyWindow(window);
		GLFW.glfwTerminate();
		window = -1;
	}
	
	public static void setPosition(int x, int y) {
		GLFW.glfwSetWindowPos(window, x, y);
	}
	
	public static void setSize(int width, int height) {
		GLFW.glfwSetWindowSize(window, width, height);
	}
	
	public static void setTitle(String title) {
		GLFW.glfwSetWindowTitle(window, title);
	}
	
	public static boolean shouldClose() {
		return GLFW.glfwWindowShouldClose(window);
	}
	
	public static float getCursorPositionX() {
		DoubleBuffer posX = BufferUtils.createDoubleBuffer(1);
		GLFW.glfwGetCursorPos(window, posX, null);
	    return (float) posX.get(0);
	}
	
	public static float getCursorPositionY() {
		DoubleBuffer posY = BufferUtils.createDoubleBuffer(1);
		GLFW.glfwGetCursorPos(window,null, posY);
	    return (float) posY.get(0);
	}
	
	public static Vector2f getCursorPosition() {
		DoubleBuffer xPosition = BufferUtils.createDoubleBuffer(1);
		DoubleBuffer yPosition = BufferUtils.createDoubleBuffer(1);
		GLFW.glfwGetCursorPos(window, xPosition, yPosition);
	    return new Vector2f((float)xPosition.get(0), (float)yPosition.get(0));
	}
	
	public static Vector2f getWindowSize() {
		try ( MemoryStack stack = MemoryStack.stackPush() ) {
			IntBuffer pWidth = stack.mallocInt(1);
			IntBuffer pHeight = stack.mallocInt(1);
			GLFW.glfwGetWindowSize(window, pWidth, pHeight);
			return new Vector2f(pWidth.get(), pHeight.get());
		}
	}
	
	public static int getWindowWidth() {
		IntBuffer winWidth = BufferUtils.createIntBuffer(1);
		GLFW.glfwGetWindowSize(window, winWidth, null);
		return winWidth.get();
	}
	
	public static int getWindowHeight() {
		IntBuffer winHeight = BufferUtils.createIntBuffer(1);
		GLFW.glfwGetWindowSize(window, null, winHeight);
		return winHeight.get();
	}
	
	///// GETTER
	
	public static long getWindowID() {return window;}
	public boolean isVisible() {return isVisible;}
	public GraphicsLoader getLoader() {return loader; }
}
