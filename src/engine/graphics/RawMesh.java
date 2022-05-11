/*
 * @Luca Marpeau - 16 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @desc
*/

package engine.graphics;

import org.lwjgl.opengl.GL11;
import org.lwjgl.opengl.GL20;
import org.lwjgl.opengl.GL30;

public class RawMesh {
	
	private int vaoID;
	private int vertexCount;
	
	private int arrayIndex;
	
	/* Constructor */
	public RawMesh(int vaoID, int vertexCount) {
		this.vaoID = vaoID;
		this.vertexCount = vertexCount;
	}
	
	/* Constructor */
	public RawMesh(int vaoID, int vertexCount, int arrayIndex) {
		this.vaoID = vaoID;
		this.vertexCount = vertexCount;
		this.arrayIndex = arrayIndex;
	}
	
	public void render() {
    	GL30.glBindVertexArray(vaoID);
		GL20.glEnableVertexAttribArray(0);
		GL11.glDrawArrays(GL11.GL_TRIANGLES, 0, vertexCount);
		GL20.glDisableVertexAttribArray(0);
		GL30.glBindVertexArray(0);
    }

	public int getVaoID() {return vaoID;}
	public int getVertexCount() {return vertexCount;}
	public int getArrayIndex() {return arrayIndex;}

	public void setVaoID(int vaoID) {this.vaoID = vaoID;}
	public void setVertexCount(int vertexCount) {this.vertexCount = vertexCount;}
	public void setArrayIndex(int arrayIndex) {this.arrayIndex = arrayIndex;}

}
