package engine.tools.maths;

import org.joml.Vector2f;

import engine.graphics.window.Window;

/**
 * @autor Luca Marpeau - 13 avr. 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @description
 * Base point for vector math. 
*/
public class Anchor {
	
	public static final Anchor UPPER_LEFT = new Anchor(-1, 1);
	public static final Anchor UPPER_RIGHT = new Anchor(1, 1);
	public static final Anchor BOTTOM_LEFT = new Anchor(-1, -1);
	public static final Anchor BOTTOM_RIGHT = new Anchor(1, -1);
	
	// Basic OpenGL Anchor
	public static final Anchor DEFAULT = new Anchor(0, 0);
	
	public float xAnchor;
	public float yAnchor;
	
	/* Constructor */
	public Anchor(float xAnchor, float yAnchor) {
		this.xAnchor = xAnchor;
		this.yAnchor = yAnchor;
	}
	
	/**
	 * Return the local position of the anchor.
	 */ 
	public Vector2f localAnchor(Vector2f size) {
		size.x = xAnchor * size.x/2;
		size.y = yAnchor * size.y/2;
		return size;
	}
	
	/**
	 * Return the translation value of the anchor for the object.
	 */
	public Vector2f translation() {
		return new Vector2f(xAnchor * Window.getWindowWidth() ,yAnchor * Window.getWindowHeight());
	}
	
	/**
	 * Change vertices anchor using current object's anchor.
	 * Take vertices array and the number of axes as the length.
	 * Return the new vertices array.
	 * 
	 */
	public float[] transpose(float[] values, float width, float height, int lenght) {
		float xTranslation = xAnchor * Window.getWindowWidth() - xAnchor * width/2;
		float yTranslation = yAnchor * Window.getWindowHeight() - yAnchor * height/2;
		
		for(int i = 0; i < values.length / lenght; i++) {
			values[i * lenght] += xTranslation;
			values[i * lenght + 1] += yTranslation;
		}
		
		return values;
	}
	
	/**
	 * Change a vector value based on his default, to the current anchor.
	 * take a Vector2f as value and return a new Vector2f value. 
	 *
	 */
	public Vector2f transpose(Vector2f value) {
		Vector2f windowSize = Window.getWindowSize();
		value.x += -xAnchor * (windowSize.x/2);
		value.y += yAnchor * (windowSize.y/2);
		return value;
	}
	
	/**
	 * Change a vector value based on his default, to the current anchor.
	 * take a Vector2f as value and an Anchor as the new anchor. 
	 * It and return a new Vector2f value. 
	 *
	 *
	public static Vector2f transpose(Vector2f value, Anchor anchor) {
		Vector2f windowSize = Window.getWindowSize();
		return new Vector2f(value.x + anchor.xAnchor * windowSize.x/2, value.y + anchor.yAnchor * windowSize.y/2);
	}
	
	/**
	 * Change a vector value based on his default, to the current anchor.
	 * take a Vector2f as value, float as xAnchor and another float as yAnchor 
	 * and return a new Vector2f value. 
	 *
	 *
	public static final Vector2f transpose(Vector2f value, float xAnchor, float yAnchor) {
		Vector2f windowSize = Window.getWindowSize();
		return new Vector2f(value.x + xAnchor * windowSize.x/2, value.y + yAnchor * windowSize.y/2);
	}
	
	/**
	 * Re-center the vector to the default anchor.
	 * 
	 */
	public static final Vector2f toCenter(Vector2f value) {
		value.x = Math.abs(value.x) - Window.getWindowWidth() / 2;
		value.y = Math.abs(value.y) - Window.getWindowHeight() / 2;
		
		return value;
	}
	
	/**
	 * Re-center the vector to the default anchor.
	 * 
	 */
	public static final Vector2f toCenter(int x, int y) {
		return new Vector2f(Math.abs(x) - Window.getWindowWidth() / 2, Math.abs(y) - Window.getWindowHeight() / 2);
	}
	
	/**
	 * Warper for ruby script. 
	 * Return anchor preset with his name.
	 */
	public static Anchor getAnchorByName(String name) {
		switch (name) {
		case "center":
			return Anchor.DEFAULT;
			
		case "upper_left":
			return Anchor.UPPER_LEFT;
			
		case "upper_right":
			return Anchor.UPPER_RIGHT;
			
		case "bottom_left":
			return Anchor.BOTTOM_LEFT;
			
		case "bottom_right":
			return Anchor.BOTTOM_RIGHT;

		default:
			return null;
		}
	}

	///// GETTERS
	
	public float xAnchor() {return xAnchor;}
	public float yAnchor() {return yAnchor;}
	
}
