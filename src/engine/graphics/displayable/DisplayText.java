/*
 * @Luca Marpeau - 25 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @desc
*/

package engine.graphics.displayable;

import java.awt.Canvas;
import java.awt.Color;
import java.awt.Font;
import java.awt.FontMetrics;
import java.awt.Graphics2D;
import java.awt.font.FontRenderContext;
import java.awt.font.GlyphVector;
import java.awt.geom.Rectangle2D;
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

public class DisplayText extends MeshObject{
	
	private String text;
	private Font font;
	private Color color;
	
	/* Constructor */
	public DisplayText(String text, Font font, String color, int layer) {
		super(layer);
		this.text = text;
		this.font = font;
		this.color = Colors.web(color);
	}
	
	/**
	 * Object initialization. 
	 * Create an image and modify it, then display it on a plane as the texture.
	 * Create texture and vertex array.
	 * 
	 */
	@Override
	public BufferedImage initialize() {
		FontMetrics fm = new Canvas().getFontMetrics(font);
		BufferedImage image = new BufferedImage(fm.stringWidth(text), fm.getMaxAscent(), BufferedImage.TYPE_INT_ARGB);
		Graphics2D g = (Graphics2D) image.getGraphics();
		
		if(size.x == 0 && size.y == 0) {
			size.x = (float) fm.stringWidth(text);
			size.y = (float) fm.getMaxAscent();
		}

		g.setColor(color);
		g.setFont(font);
		g.drawString(text, 0, g.getFontMetrics().getMaxAscent() - g.getFontMetrics().getMaxDescent());
		
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
	
	public String getText() {return text;}
	public Font getFont() {return font;}
	public Color getTextColor() {return color;}
	
	///// SETTERS 
	public void setText(String text) {
		this.text = text;
		this.updateMesh();
	}

}
