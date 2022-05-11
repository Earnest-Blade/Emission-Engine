/*
 * @Luca Marpeau - 26 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @desc
*/

package engine.graphics.displayable;

import java.awt.Canvas;
import java.awt.Color;
import java.awt.Font;
import java.awt.FontMetrics;
import java.awt.Graphics;
import java.awt.image.BufferedImage;

import org.joml.Vector2f;
import org.lwjgl.glfw.GLFW;
import org.lwjgl.opengl.GL11;
import org.lwjgl.opengl.GL13;
import org.lwjgl.opengl.GL20;
import org.lwjgl.opengl.GL30;

import engine.graphics.MeshObject;
import engine.graphics.shaders.StaticShader;
import engine.graphics.window.Window;
import engine.loaders.ResourceLoader;
import engine.tools.Colors;
import engine.tools.Printer;
import engine.tools.maths.Anchor;
import engine.tools.maths.Maths;

public abstract class DisplayButton extends MeshObject{
	
	private String text;
	private Font font;
	private Color color;
	private BufferedImage backgroundImage;
	
	private float xOffset, yOffset;
	private boolean isHover;
	
	/* Constructor */
	public DisplayButton(String text, Font font,String color, String backgroundImage, int width, int height, int layer) {
		super(layer);
		this.text = text;
		this.font = font;
		
		// Background loading
		if(backgroundImage.isEmpty() || backgroundImage == null) this.backgroundImage = null;	
		else this.backgroundImage = ResourceLoader.loadImage(backgroundImage);
		
		this.size = new Vector2f(width, height);
		this.xOffset = 0;
		this.yOffset = 0;
		this.color = Colors.web(color);
	}
	
	public abstract void OnHover();
	public abstract void AfterHover();
	public abstract void OnClick();

	/**
	 * Object initialization. 
	 * Create an image and modify it, then display it on a plane as the texture.
	 * Create texture and vertex array.
	 * 
	 */
	@Override
	public BufferedImage initialize() {
		if(size.x == 0 && size.y == 0) {
			FontMetrics fm = new Canvas().getFontMetrics(font);
			size.x = (float) fm.stringWidth(text);
			size.y = (float) fm.getMaxAscent();
		}
		
		BufferedImage image = new BufferedImage((int)size.x, (int)size.y, BufferedImage.TYPE_INT_RGB);
		Graphics graphics = image.getGraphics();
		
		if(backgroundImage != null) graphics.drawImage(backgroundImage, 0, 0, null);

		graphics.setColor(color);
		graphics.setFont(font);
		graphics.drawString(text, (int)(0 + xOffset),(int)(graphics.getFontMetrics().getMaxAscent() - graphics.getFontMetrics().getMaxDescent() + yOffset));
		
		graphics.dispose();
		
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
	public void update() {
		if(isHover()) {
			// TODO : When cursor hovering button
			if(!isHover) OnHover();
			isHover = true;
			
			if(GLFW.glfwGetMouseButton(Window.getWindowID(), GLFW.GLFW_MOUSE_BUTTON_1) == GLFW.GLFW_PRESS) {
				// TODO : When cursor clicking button
				OnClick();
			}
		}
		else if(isHover) {
			isHover = false;
			AfterHover();
		}
	}
	
	/*
	 * Return if the cursor is hovering the button.
	 * It apply anchor to cursor's position to compare positions.
	 */
	private boolean isHover() { 
		Vector2f cursor = Window.getCursorPosition();
		cursor = anchor.transpose(Anchor.toCenter(cursor));
	
		return (cursor.x >= getPosition().x + -size.x/2 && cursor.x <= getPosition().x + size.x/2)
		&& (cursor.y >= getPosition().y + -size.y/2 && cursor.y <= getPosition().y + size.y/2);
	}
	
	///// GETTERS
	
	public Vector2f getOffset() { return new Vector2f(xOffset, yOffset); }
	
	///// SETTER
	
	public void setOffset(int xOffset, int yOffset) {
		this.xOffset = xOffset;
		this.yOffset = yOffset;
		
		updateMesh();
	}
	
	public void setText(String text) {
		this.text = text;
		
		updateMesh();
	}
	
	public void setColor(String color) {
		this.color = Colors.web(color);
		
		updateMesh();
	}
	
	public void setBackground(String path) {
		if(path.isEmpty() || path == null) this.backgroundImage = null;	
		else this.backgroundImage = ResourceLoader.loadImage(path);
		
		updateMesh();
	}
}
