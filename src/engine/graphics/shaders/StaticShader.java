/*
 * @Luca Marpeau - 16 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @desc
*/

package engine.graphics.shaders;

import org.joml.Matrix4f;

public class StaticShader extends ShaderProgram{
	
	private static final String VERTEX_FILE = "assets/shaders/vertexShader.GLSL";
	private static final String FRAGMENT_FILE = "assets/shaders/fragmentShader.GLSL";

	public StaticShader() {
		super(VERTEX_FILE, FRAGMENT_FILE);
	}

	@Override
	protected void bindAttributes() {
		super.bindAttribute(0, "position");
		super.bindAttribute(1, "textureCoords");
		super.bindAttribute(2, "textureAlpha");
	}

	// TODO: Changer parce qu'a chaque actualisation, on ajoute un élément à la liste.
	public void newUniform(String name, Matrix4f value) {
		createUniform(name);
		setUniform(name, value);
	}
	
	public void newUniform(String name, float value) {
		createUniform(name);
		setUniform(name, value);
	}

}
