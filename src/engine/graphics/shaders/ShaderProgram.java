/*
 * @Luca Marpeau - 16 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @desc
*/

package engine.graphics.shaders;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.nio.FloatBuffer;
import java.util.HashMap;

import org.joml.Matrix4f;
import org.joml.Vector3f;
import org.lwjgl.BufferUtils;
import org.lwjgl.opengl.GL11;
import org.lwjgl.opengl.GL20;
import org.lwjgl.system.MemoryStack;
import org.lwjgl.system.MemoryUtil;

public abstract class ShaderProgram {
	
	private int programID;
	private int vertexShaderID;
	private int fragmentShaderID;
	
	private HashMap<String, Integer> uniforms;
	
	public ShaderProgram(String vertexFile, String fragmentFile) {
		vertexShaderID = loadShader(vertexFile, GL20.GL_VERTEX_SHADER);
		fragmentShaderID = loadShader(fragmentFile, GL20.GL_FRAGMENT_SHADER);
		programID = GL20.glCreateProgram();
		uniforms = new HashMap<String, Integer>();
		
		GL20.glAttachShader(programID, vertexShaderID);
		GL20.glAttachShader(programID, fragmentShaderID);
		
		bindAttributes();
		
		GL20.glLinkProgram(programID);
		GL20.glValidateProgram(programID);
	}
	
	public void start() {
		GL20.glUseProgram(programID);
	}
	
	public void stop() {
		GL20.glUseProgram(0);
	}
	
	public void clear() {
		stop();
		GL20.glDetachShader(programID, vertexShaderID);
		GL20.glDetachShader(programID, fragmentShaderID);
		GL20.glDeleteShader(vertexShaderID);
		GL20.glDeleteShader(fragmentShaderID);
		GL20.glDeleteProgram(programID);
	}
	
	protected abstract void bindAttributes();
	
	protected void bindAttribute(int attribute, String variableName) {
		GL20.glBindAttribLocation(programID, attribute, variableName);
	}
	
	protected void createUniform(String uniformName) {
	    int uniformLocation = GL20.glGetUniformLocation(programID,  uniformName);
	    if (uniformLocation < 0) {
	        System.err.println("ERROR: Could not find uniform variable '" + uniformName + "'!");
	    }
	    uniforms.put(uniformName, uniformLocation);
	}
	
	public void setUniform(String uniformName, float value) {
		GL20.glUniform1f(uniforms.get(uniformName), value);
	}
	
	public void setUniform(String uniformName, Matrix4f value) {
	    try (MemoryStack stack = MemoryStack.stackPush()) {
	        FloatBuffer fb = stack.mallocFloat(16);
	        value.get(fb);
	        GL20.glUniformMatrix4fv(uniforms.get(uniformName), false, fb);
	    }
	}
	
	private static int loadShader(String file, int type) {
		StringBuilder shaderSource = new StringBuilder();
		
		try{
			BufferedReader reader = new BufferedReader(new FileReader(file));
			String line;
			
			while((line = reader.readLine())!=null){
				shaderSource.append(line).append("//\n");
			}
			
			reader.close();
			  
		}catch(IOException e){
			e.printStackTrace();
			System.exit(-1);
		}
		
		int shaderID = GL20.glCreateShader(type);
		GL20.glShaderSource(shaderID, shaderSource);
		GL20.glCompileShader(shaderID);
		
		if(GL20.glGetShaderi(shaderID, GL20.GL_COMPILE_STATUS )== GL11.GL_FALSE){
			System.out.println(GL20.glGetShaderInfoLog(shaderID, 500));
			System.err.println("ERROR: Could not compile shader!");
			System.exit(-1);
		}
		
		return shaderID;
	}

}
