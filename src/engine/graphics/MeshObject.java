package engine.graphics;

import java.awt.image.BufferedImage;
import java.util.Arrays;

import org.joml.Vector2f;
import org.lwjgl.opengl.GL11;
import org.lwjgl.opengl.GL13;
import org.lwjgl.opengl.GL20;
import org.lwjgl.opengl.GL30;

import engine.data.GuiData;
import engine.graphics.shaders.StaticShader;
import engine.graphics.window.Renderer;
import engine.graphics.window.Window;
import engine.loaders.GraphicsLoader;
import engine.tools.maths.Anchor;
import engine.tools.maths.Maths;

/**
 * @autor Luca Marpeau - 17 mars 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @description
 * This class represent all entity in the application.
 * It's an abstract class.
*/

public abstract class MeshObject {
	
	protected Mesh model;
	protected Vector2f position;
	protected Vector2f size;
	protected Anchor anchor;
	protected float rotX, rotY;
	protected float scale;
	protected int layer;
	
	protected boolean isVisible;
	protected boolean isInitiate;
	
	/* Constructor */
	public MeshObject(int layer) {
		this.size = new Vector2f();
		this.layer = layer;
		this.anchor = Anchor.UPPER_LEFT;
		this.position = new Vector2f();
	}
	
	/* Constructor */
	public MeshObject(Mesh model, int layer) {
		this.model = model;
		this.position = new Vector2f();
		this.size = new Vector2f();
		this.anchor = Anchor.UPPER_LEFT;
		this.rotX = 0;
		this.rotY = 0;
		this.scale = 1;
		this.isInitiate = true;
		this.layer = layer;
	}
	
	/* Constructor */
	public MeshObject(Mesh model, Vector2f position, float rotX, float rotY, float scale, int layer) {
		this.model = model;
		this.position = position;
		this.size = new Vector2f();
		this.anchor = Anchor.UPPER_LEFT;
		this.rotX = rotX;
		this.rotY = rotY;
		this.scale = scale;
		this.isInitiate = true;
		this.layer = layer;
	}
	
	
	/**
	 * Create the object in Graphic Thread. 
	 * Call from the Loading Queue
	 * 
	 */
	public abstract BufferedImage initialize();
	
	/**
	 * Render the object using OpenGL.
	 * 
	 */
	public abstract void render(StaticShader shader);
	
	/**
	 * Execute one time peer frame. Update Object.
	 * 
	 */
	public abstract void update();
	
	public void display(StaticShader shader) {
		GL30.glBindVertexArray(model.getRawModel().getVaoID());
		GL20.glEnableVertexAttribArray(0);
		GL20.glEnableVertexAttribArray(1);

		shader.newUniform("transformation", Maths.createTransformationMatrix(getPosition(), layer, rotX, rotY, scale));
		//shader.newUniform("textureAlpha", model.getAlpha());
		shader.newUniform("layer", layer);
		
		GL13.glActiveTexture(GL13.GL_TEXTURE0);
		GL11.glBindTexture(GL11.GL_TEXTURE_2D, model.getTexture().getTextureID());
		GL11.glDrawElements(GL11.GL_TRIANGLES, model.getRawModel().getVertexCount(), GL11.GL_UNSIGNED_INT, 0);
		GL20.glDisableVertexAttribArray(0);
		GL20.glDisableVertexAttribArray(1);
		GL30.glBindVertexArray(0);
	}
	
	/**
	 * Create Object vertex.
	 */
	public float[] vertex() {
		float[] vertices = {
				-size.x/2, 	size.y/2, 	0,
				-size.x/2, 	-size.y/2,  0,
				size.x/2, 	-size.y/2,  0,
				size.x/2, 	size.y/2, 	0
			};
		
		vertices = Maths.normalize(anchor.transpose(vertices, size.x, size.y, 3), 3);
		return vertices;
    }
	
	/**
	 * Set all object's @param with the default value.
	 * Then it's add object to the loading Queue.
	 */
	public void construct(float[] vertices, BufferedImage image) {
		this.model = new Mesh(vertices, GraphicsLoader.QUAD_TEXTURE_CORDS, GraphicsLoader.ORDER_INDICES, image);
		if(position == null) this.position = new Vector2f();
		if(rotX == 0) this.rotX = 0;
		if(rotY == 0) this.rotY = 0;
		if(scale == 0) this.scale = 1;
		this.isInitiate = true;
		
		addToRender();
	}
	
	/**
	 * Unload object's vbos and texture.
	 */
	public void unload() {
		if(model != null) {
			GraphicsLoader.getLoader().clear(model.getRawModel().getArrayIndex());
		}
	}
	
	public void destroy() {
		
	}
	
	public void show() {
		this.isVisible = true;
	}
	
	public void hide() {
		this.isVisible = false;
	}
	
	/*
	public void increasePosition(float x, float y) {
		this.position.x += x;
		this.position.y += y;
	}
	
	public void increaseRotation(float rotX, float rotY) {
		this.rotX += rotX;
		this.rotY += rotY;
	}*/
	
	public void updateMesh() {
		unload();
		
		Renderer.RENDER_QUEUE.remove(this);
		Renderer.RENDER_LOADING_QUEUE.add(this);
	}
	
	/*
	 * Add object to the Rendering Queue.
	 */
	public void addToRender() {
    	Renderer.RENDER_QUEUE.add(this);
    }
    
    public void removeFromRender() {
    	Renderer.RENDER_QUEUE.remove(this);
    }
    
    ///// GETTERS
    
	public Mesh getModel() {return model;}
	public float getRotX() {return rotX;}
	public float getRotY() {return rotY;}
	public float getScale() {return scale;}
	public boolean isVisible() {return isVisible; }
	public boolean isInitiate() {return isInitiate; }
	public float getWidth() {return size.x;}
	public float getHeight() {return size.y;}
	public Vector2f getSize() {return size;}
	public int getLayer() {return layer;}
	public Vector2f getPosition() {
		return new Vector2f(-anchor.xAnchor * position.x, 
							-anchor.yAnchor * position.y);
	}
	
	///// SETTERS
	
	public void setModel(Mesh model) {this.model = model;}
	public void setPosition(Vector2f position) {this.position = position;}
	public void setPosition(float x, float y) {this.position.x = x; this.position.y = y;}
	public void setRotX(float rotX) {this.rotX = rotX;}
	public void setRotY(float rotY) {this.rotY = rotY;}
	public void setRotation(float rotX, float rotY) {this.rotX = rotX; this.rotY = rotY;}
	public void setScale(float scale) {this.scale = scale;}
	public void setVisible(boolean isVisible) {this.isVisible = isVisible;}
	public void setInitiate(boolean isInitiate) {this.isInitiate = isInitiate;}
	public void setLayer(int layer) {
		this.layer = layer;
		this.updateMesh();
	}
	public void setSize(float width, float height) {
    	this.size.x = width;
    	this.size.y = height;
    	this.updateMesh();
    }
	public void setAnchor(Anchor anchor) {
		this.anchor = anchor;
		this.updateMesh();
	}
}
