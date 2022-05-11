/*
 * @Luca Marpeau - 16 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @desc
*/

package engine.graphics.displayable;

import java.awt.image.BufferedImage;

import javax.crypto.spec.PSource;

import org.joml.Matrix4f;
import org.joml.Vector2f;
import org.joml.Vector3f;
import org.lwjgl.opengl.GL11;
import org.lwjgl.opengl.GL13;
import org.lwjgl.opengl.GL20;
import org.lwjgl.opengl.GL30;

import engine.graphics.MeshObject;
import engine.graphics.shaders.StaticShader;
import engine.graphics.window.Renderer;
import engine.graphics.window.Window;
import engine.loaders.GraphicsLoader;
import engine.loaders.ResourceLoader;
import engine.tools.maths.Maths;

public class DisplayImage extends MeshObject{
	
	private String path;

	/* Constructor */
	public DisplayImage(String path, int layer) {
		super(layer);
		this.path = path;
	}
	
	/**
	 * Object initialization. 
	 * Set object size using BufferedImage's size.
	 * Create texture and vertex array.
	 * 
	 */
	@Override
	public BufferedImage initialize() {
		BufferedImage image = ResourceLoader.loadImage(path);
		if(size.x == 0 && size.y == 0) {
			size.x = image.getWidth();
			size.y = image.getHeight();
		}
		
		this.construct(super.vertex(), image);
		
		return image;
	}

	/**
	 * Render the object using OpenGL.
	 * Add the Transformation Uniform to the StaticShader.
	 * 
	 */
	@Override
	public void render(StaticShader shader) {
		super.display(shader);
	}

	/**
	 * Execute one time peer frame.
	 * Used to update Object.
	 */
	@Override
	public void update() {}
	
	///// GETTERS
	
	public void changeTexture(String path) {
		this.path = path;
		Renderer.RENDER_LOADING_QUEUE.add(this);
	}

	public String getPath() {return path;}
}
