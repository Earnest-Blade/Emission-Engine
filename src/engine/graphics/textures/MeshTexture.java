/*
 * @Luca Marpeau - 16 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @desc
*/

package engine.graphics.textures;

import org.joml.Vector4f;

public class MeshTexture {
	
	private int textureID;
	
	public MeshTexture(int textureID) {
		this.textureID = textureID;
	}

	public int getTextureID() {return textureID;}

}
