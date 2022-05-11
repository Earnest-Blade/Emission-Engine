/*
 * @Luca Marpeau - 16 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @desc
*/

package engine.loaders;

import java.awt.image.BufferedImage;
import java.nio.FloatBuffer;
import java.nio.IntBuffer;
import java.util.ArrayList;
import java.util.List;

import org.joml.Vector4f;
import org.lwjgl.BufferUtils;
import org.lwjgl.opengl.GL;
import org.lwjgl.opengl.GL11;
import org.lwjgl.opengl.GL15;
import org.lwjgl.opengl.GL20;
import org.lwjgl.opengl.GL30;

import engine.Application;
import engine.graphics.RawMesh;

public class GraphicsLoader {
	
	public static final int[] ORDER_INDICES = {1, 1, 0, 2, 1, 3};
	public static final float[] QUAD_TEXTURE_CORDS = { 0,0,0,1,1,1,1,0 };
	
	private List<Integer> vaos = new ArrayList<Integer>();
	private List<Integer> vbos = new ArrayList<Integer>();
	private ArrayList<Integer> textures = new ArrayList<Integer>();

	public RawMesh loadToVAO(float[] positions, float[] textureCoords, int[] indices) {
		int vaoID = createVAO();
		bindIndicesBuffer(indices);
		store3DDataInAttributeList(0, positions);
		store2DDataInAttributeList(1, textureCoords);
		unbindVAO();
		return new RawMesh(vaoID, indices.length, vaos.size() - 1);
	}
	
	public int loadTexture(String fileName) {
		int textureID = ResourceLoader.loadTexture(fileName);
		textures.add(textureID);
		return textureID;
	}
	
	public int loadTexture(BufferedImage image) {
		int textureID = ResourceLoader.loadTexture(image);
		textures.add(textureID);
		return textureID;
	}
	
	public void clear() {
		for(int vao : vaos) GL30.glDeleteVertexArrays(vao);
		for(int vbo : vbos) GL15.glDeleteBuffers(vbo);
		for(int texture : textures) GL11.glDeleteTextures(texture);
	}
	
	public void clear(int index) {
		GL15.glDeleteBuffers(vbos.get(index));
		GL11.glDeleteTextures(index);
	}
	
	private int createVAO() {
		int vaoID = GL30.glGenVertexArrays();
		vaos.add(vaoID);
		GL30.glBindVertexArray(vaoID);
		return vaoID;
	}
	
	private void store2DDataInAttributeList(int attributeNumber, float[] data) {
		int vboID = GL15.glGenBuffers();
		vbos.add(vboID);
		GL15.glBindBuffer(GL15.GL_ARRAY_BUFFER, vboID);
		FloatBuffer buffer = storeDataInFloatBuffer(data);
		GL15.glBufferData(GL15.GL_ARRAY_BUFFER, buffer, GL15.GL_STATIC_DRAW);
		GL20.glVertexAttribPointer(attributeNumber, 2, GL11.GL_FLOAT, false, 0, 0);
		GL15.glBindBuffer(GL15.GL_ARRAY_BUFFER, 0);
	}
	
	private void store3DDataInAttributeList(int attributeNumber, float[] data) {
		int vboID = GL15.glGenBuffers();
		vbos.add(vboID);
		GL15.glBindBuffer(GL15.GL_ARRAY_BUFFER, vboID);
		FloatBuffer buffer = storeDataInFloatBuffer(data);
		GL15.glBufferData(GL15.GL_ARRAY_BUFFER, buffer, GL15.GL_STATIC_DRAW);
		GL20.glVertexAttribPointer(attributeNumber, 3, GL11.GL_FLOAT, false, 0, 0);
		GL15.glBindBuffer(GL15.GL_ARRAY_BUFFER, 0);
	}

	private void unbindVAO() {
		GL30.glBindVertexArray(0);
	}
	
	private void bindIndicesBuffer(int[] indices) {
		int vboID = GL15.glGenBuffers();
		vbos.add(vboID);
		GL15.glBindBuffer(GL15.GL_ELEMENT_ARRAY_BUFFER, vboID);
		IntBuffer buffer = storeDataInIntBuffer(indices);
		GL15.glBufferData(GL15.GL_ELEMENT_ARRAY_BUFFER, buffer, GL15.GL_STATIC_DRAW);
		
	}
	
	private FloatBuffer storeDataInFloatBuffer(float[] data) {
		FloatBuffer buffer = BufferUtils.createFloatBuffer(data.length);
		buffer.put(data);
		buffer.flip();
		return buffer;
	}
	
	private IntBuffer storeDataInIntBuffer(int[] data) {
		IntBuffer buffer = BufferUtils.createIntBuffer(data.length);
		buffer.put(data);
		buffer.flip();
		return buffer;
	}

	public static GraphicsLoader getLoader() {
		return Application.CURRENT_APP.getWindow().getLoader();
	}
	
}
