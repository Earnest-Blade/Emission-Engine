package engine.tools;

import java.awt.Color;

import org.joml.Vector4f;

/**
 * @autor Luca Marpeau - 20 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @description
 * This is color toolkit. 
 * Used to use java.awt.Color class with OpenGL Color method.
*/
public interface Colors {
	
	/* Vector4f colors */
	Vector4f WHITE = new Vector4f(1, 1, 1, 1);
	Vector4f BLACK = new Vector4f(0, 0, 0, 0);
	
	/**
	 * This method transform a web color to an java.awt.Color object.
	 */
	public static Color web(String name) {
		return Color.decode(name);
	}
}
