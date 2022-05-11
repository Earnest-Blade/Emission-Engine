package engine.graphics.displayable;

import java.awt.Font;
import java.awt.Graphics;
import java.awt.image.BufferedImage;
import java.util.ArrayList;

import org.lwjgl.opengl.GL11;
import org.lwjgl.opengl.GL13;
import org.lwjgl.opengl.GL20;
import org.lwjgl.opengl.GL30;

import engine.graphics.MeshObject;
import engine.graphics.shaders.StaticShader;
import engine.graphics.window.Window;
import engine.loaders.ResourceLoader;
import engine.tools.Colors;
import engine.tools.Toolkit;
import engine.tools.maths.Maths;

/**
 * @autor 
 * Luca Marpeau - 22 avr. 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @description
 * Unlike other display class, this one hasn't a warper in Ruby. So the syntax isn't classic java syntax
 * but Ruby's syntax in method's name.
*/
public class DisplayScreen extends MeshObject {
	
	private ArrayList<MeshObject> components = new ArrayList<MeshObject>();
	
	private BufferedImage buffImage;
	private Graphics graphics;
	
	private Font defaultFont;
	
	/* Constructor */
	public DisplayScreen(int layer) {
		super(layer);
		
		buffImage = new BufferedImage(Window.getWindowWidth() * 2, Window.getWindowHeight() * 2 , BufferedImage.TYPE_INT_ARGB);
		graphics = buffImage.getGraphics();
	}

	/**
	 * Object initialization. 
	 * Create an image and modify it, then display it on a plane as the texture.
	 * Create texture and vertex array.
	 * 
	 */
	@Override
	public BufferedImage initialize() {
		
		if(size.x == 0 && size.y == 0) {
			size.x = buffImage.getWidth();
			size.y = buffImage.getHeight();
		}
		
		graphics.dispose();
		
		this.construct(this.vertex(), buffImage);
		
		return buffImage;
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
	 * 
	 */
	@Override
	public void update() {}
	
	/**
	 * This method set object's visibility as true.
	 * it's Overriding GameObject's method.
	 * 
	 */
	@Override
	public void show() {
		for (MeshObject gameObject : components) {
			gameObject.show();
		}
		
		super.show();
	}
	
	/**
	 * This method set object's visibility as false.
	 * it's Overriding GameObject's method.
	 * 
	 */
	@Override
	public void hide() {
		for (MeshObject gameObject : components) {
			gameObject.hide();
		}
		
		super.hide();
	}
	
	public void add(MeshObject object) {
		object.updateMesh();
		components.add(object);
	}
	
	public void addImage(String path) {
		graphics.drawImage(ResourceLoader.loadImage(path), 0, 0, null);
	}
	
	public void addImage(String path, int y, int x) {
		graphics.drawImage(ResourceLoader.loadImage(path), x, y, null);
	}
	
	public void addImage(String path, int y, int x, int width, int height) {
		graphics.drawImage(Toolkit.resizeBufferedImage(ResourceLoader.loadImage(path), width * 2, height * 2), x, y, null);
	}
	
	public void addText(String text, String color, int size) {
		graphics.setColor(Colors.web(color));
		graphics.setFont(defaultFont.deriveFont(size));
		graphics.drawString(text, 0, graphics.getFontMetrics().getMaxAscent() - graphics.getFontMetrics().getMaxDescent());
	}
	
	public void setDefaultFont(Font font) {
		this.defaultFont = font;
	}
	
}
