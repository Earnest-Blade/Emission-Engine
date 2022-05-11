package engine.tools.maths;

import org.joml.Matrix4f;
import org.joml.Vector2f;
import org.joml.Vector3f;

import engine.graphics.window.Window;

/**
 * @autor Luca Marpeau - 16 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @description
 * This class is final and contain all application necessary math functions.
 * All functions must be static so that all classes can have access to.
 * 
*/
public final class Maths {
	
	/**
	 * Create a matrix that contain the position, the layer, the xRotation, the yRotation and the scale of an object.
	 * It return the new transform matrix.
	 * 
	 */
	public static Matrix4f createTransformationMatrix(Vector2f position, int layer, float rx, float ry, float scale) {
		Matrix4f matrix = new Matrix4f();
		matrix.identity();
		matrix.translate(position.x, position.y, 0);
		matrix.rotate((float)Math.toRadians(rx), new Vector3f(1,0,0));
		matrix.rotate((float)Math.toRadians(ry), new Vector3f(0,0,1));
		matrix.rotate((float)Math.toRadians(0), new Vector3f(0,1,0));
		matrix.scale(new Vector3f(scale, scale, scale));
		return matrix;
	}
	
	/**
	 * 
	 * 
	 * @return
	 */
	public static Matrix4f createProjectionMatrix(float FOV, float maxDistance, float minDistance) {
		float aspectRatio = Window.getWindowWidth() / Window.getWindowHeight();
		float yScale =  (float) ((1f / Math.tan(Math.toRadians(FOV / 2f))) * aspectRatio);
		float xScale = yScale / aspectRatio;
		float frustum_lenght = maxDistance - minDistance;
		
		Matrix4f matrix = new Matrix4f();
		matrix.m00(xScale);
		matrix.m11(yScale);
		matrix.m22(-((maxDistance + minDistance) / frustum_lenght));
		matrix.m23(-1f);
		matrix.m32(-((2 * minDistance * maxDistance) / frustum_lenght));
		matrix.m33(0);
		return matrix;
	}
	
	/**
	 * Transform a screen vertices list coords to OpenGL vertices list coords.
	 * It's take a float list and the number of axes.
	 */
	public static float[] normalize(float[] vertices, int lenght) {
		for(int i = 0; i < vertices.length / lenght; i++) {
			vertices[i * lenght] /= Window.getWindowWidth();
			vertices[i * lenght + 1] /= Window.getWindowHeight();
		}

		return vertices;
	}
	
	/**
	 * Transform a vector to OpenGL type vector coords.
	 * It's take a Vector2f as the value.
	 */
	public static Vector2f normalize(Vector2f vector) {
		vector.x = vector.x * 2 / Window.getWindowWidth();
		vector.y = vector.y * 2 / Window.getWindowHeight();
		return vector;
	}
	
	/**
	 * Transform an OpenGL type vector coords vector to a normal vector.
	 * It's take a Vector2f as the value.
	 */
	public static Vector2f unNormalize(Vector2f vector) {
		vector.x = vector.x * Window.getWindowWidth();
		vector.y = vector.y * Window.getWindowHeight();
		return vector;
	}
	
	/**
	 * Simple math clamping.
	 * 
	 */
	public static float clamp(float val, float min, float max) {
	    return Math.max(min, Math.min(max, val));
	}
	
	
}
