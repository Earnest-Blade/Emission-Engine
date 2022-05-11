/*
 * @Luca Marpeau - 16 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @desc
*/

package engine.graphics;

import java.awt.image.BufferedImage;

import org.joml.Vector4f;
import org.lwjgl.opengl.GL11;
import org.lwjgl.opengl.GL13;
import org.lwjgl.opengl.GL20;
import org.lwjgl.opengl.GL30;

import engine.Application;
import engine.graphics.RawMesh;
import engine.graphics.textures.MeshTexture;
import engine.loaders.GraphicsLoader;

public class Mesh {
	
	private RawMesh rawModel;
	private MeshTexture texture;
	
	private float[] positions, textureCoords;
	private int[] indices;
	
	private float alpha = 1;
	private Vector4f color;
	
	/* Constructor */
	public Mesh(RawMesh rawModel, MeshTexture texture) {
		this.rawModel = rawModel;
		this.texture = texture;
	}
	
	/* Constructor */
	public Mesh(float[] positions, float[] textureCoords, int[] indices, String path) {
		this.positions = positions;
		this.textureCoords = textureCoords;
		this.indices = indices;
		this.rawModel = Application.CURRENT_APP.getWindow().getLoader().loadToVAO(positions, textureCoords, indices);
		this.texture = new MeshTexture(GraphicsLoader.getLoader().loadTexture(path));
	}
	
	/* Constructor */
	public Mesh(float[] positions, float[] textureCoords, int[] indices, BufferedImage image) {
		this.positions = positions;
		this.textureCoords = textureCoords;
		this.indices = indices;
		this.rawModel = Application.CURRENT_APP.getWindow().getLoader().loadToVAO(positions, textureCoords, indices);
		this.texture = new MeshTexture(GraphicsLoader.getLoader().loadTexture(image));
	}
	
	public void render() {
		GL30.glBindVertexArray(rawModel.getVaoID());
		GL20.glEnableVertexAttribArray(0);
		GL20.glEnableVertexAttribArray(1);
		GL13.glActiveTexture(GL13.GL_TEXTURE0);
		GL11.glBindTexture(GL11.GL_TEXTURE_2D, texture.getTextureID());
		GL11.glDrawElements(GL11.GL_QUADS, rawModel.getVertexCount(), GL11.GL_UNSIGNED_INT, 0);
		GL20.glDisableVertexAttribArray(0);
		GL20.glDisableVertexAttribArray(1);
		GL30.glBindVertexArray(0);
	}

	public RawMesh getRawModel() {return rawModel;}
	public MeshTexture getTexture() {return texture;}
	public float getAlpha() {return alpha;}
	public Vector4f getColor() {return color;}
	
	public float[] getPositions() {return positions;}
	public float[] getTextureCoords() {return textureCoords;}
	public int[] getIndices() {return indices;}

	public void setAlpha(float alpha) {this.alpha = alpha;}
}
