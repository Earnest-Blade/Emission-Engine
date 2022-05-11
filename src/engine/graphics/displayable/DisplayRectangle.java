/*
 * @Luca Marpeau - 25 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @desc
*/

package engine.graphics.displayable;

import java.awt.Color;
import java.awt.Graphics;
import java.awt.image.BufferedImage;

import org.joml.Vector2f;
import org.lwjgl.opengl.GL11;
import org.lwjgl.opengl.GL13;
import org.lwjgl.opengl.GL20;
import org.lwjgl.opengl.GL30;

import engine.graphics.MeshObject;
import engine.graphics.shaders.StaticShader;
import engine.graphics.window.Renderer;
import engine.graphics.window.Window;
import engine.loaders.GraphicsLoader;
import engine.tools.Colors;
import engine.tools.maths.Maths;

public class DisplayRectangle extends MeshObject{
	
	private Color color;
	private boolean isFill;
	
	/* Constructor */
	public DisplayRectangle(int width, int height, String webColor, boolean isFill, int layer) {
		super(layer);
		size.x = width;
		size.y = height;
		this.color = Colors.web(webColor);
		this.isFill = isFill;
	}
	
	/**
	 * Object initialization. 
	 * Create an image and modify it, then display it on a plane as the texture.
	 * Create texture and vertex array.
	 * 
	 */
	@Override
	public BufferedImage initialize() {
		BufferedImage image = new BufferedImage((int)size.x, (int)size.y, BufferedImage.TYPE_INT_ARGB);
		Graphics g = image.getGraphics();
		
		g.setColor(color);
		
		if(isFill) g.fillRect(0, 0, (int)size.x, (int)size.y);
		else g.drawRect(0, 0, (int)size.x, (int)size.y);
		
		g.dispose();
		
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
	
	public Color getColor() {return color;}
	public boolean isFill() {return isFill;}
	
	
}
