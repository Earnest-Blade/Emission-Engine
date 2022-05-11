package engine.graphics.window;

import java.util.ArrayList;
import java.util.ConcurrentModificationException;
import java.util.LinkedList;
import java.util.Queue;

import org.joml.Vector4f;
import org.lwjgl.opengl.GL;
import org.lwjgl.opengl.GL11;
import org.lwjgl.opengl.GLUtil;

import engine.Application;
import engine.graphics.MeshObject;
import engine.graphics.shaders.StaticShader;
import engine.tools.Colors;

public class Renderer {

	public static final Queue<MeshObject> RENDER_LOADING_QUEUE = new LinkedList<MeshObject>();

	public static final ArrayList<MeshObject> RENDER_QUEUE = new ArrayList<MeshObject>();

	/* Constructor */
	public Renderer() {
		Renderer.setClearColor(Colors.BLACK);
		GL11.glEnable(GL11.GL_BLEND);
		GL11.glBlendFunc(GL11.GL_SRC_ALPHA, GL11.GL_ONE_MINUS_SRC_ALPHA);
		GL11.glMatrixMode(GL11.GL_PROJECTION);
		GL11.glPushMatrix();
		GL11.glOrtho(0, Application.CURRENT_APP.getApplicationGUIData().window_width,
					Application.CURRENT_APP.getApplicationGUIData().window_height, 0,
					0, 0);
	}

	public static void renderQueue(StaticShader shader) {
		GL11.glLoadIdentity();
		try {
			for (MeshObject object : RENDER_QUEUE) {
				if (object.isVisible() && object.isInitiate()) {
					object.update();
					object.render(shader);
				}
			}
		}
		catch(ConcurrentModificationException e) {}
	}

	/**
	 * This method poll RENDER_LOADING_QUEUE to initialize in the Graphic Thread all elements.
	 * This method will stop if the Queue is empty
	 * It's execute every frame.  
	 * 
	 */
	public static void loadQueue() {
		if (RENDER_LOADING_QUEUE.isEmpty()) return;
		for (int i = 0; i < RENDER_LOADING_QUEUE.size(); i++) {
			RENDER_LOADING_QUEUE.poll().initialize();
		}
	}

	/**
	 * Clear the window to render a new frame.
	 * 
	 */
	public static void clear() {
		GL11.glClear(GL11.GL_COLOR_BUFFER_BIT | GL11.GL_DEPTH_BUFFER_BIT);
	}

	/**
	 * Define the color used to clear the screen.
	 * 
	 */
	public static void setClearColor(Vector4f color) {
		GL11.glClearColor(color.x, color.y, color.z, color.w);
	}
}
