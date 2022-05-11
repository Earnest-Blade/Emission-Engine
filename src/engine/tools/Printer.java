/**
 * @autor 
 * Luca Marpeau - 16 avr. 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @description
*/

package engine.tools;

import java.util.Arrays;

import org.joml.Vector2f;

// TODO : Remove this from the final build
public final class Printer {
	
	public static void print(Vector2f a) {
		System.out.println("[ x : " + a.x + " ; y :" + a.y + " ]");
	}
	
	public static void print(Object a) {
		System.out.println(a.toString());
	}
	
	public static void print(float[] a) {
		System.out.println(Arrays.toString(a));
	}
	
	public static void print(String[] a) {
		System.out.println(Arrays.toString(a));
	}
}
