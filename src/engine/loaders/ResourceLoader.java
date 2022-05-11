package engine.loaders;

import java.awt.Font;
import java.awt.FontFormatException;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.IntBuffer;

import javax.imageio.ImageIO;

import org.lwjgl.BufferUtils;
import org.lwjgl.glfw.GLFWImage;
import org.lwjgl.opengl.GL11;
import org.lwjgl.opengl.GL12;
import org.lwjgl.stb.STBImage;
import org.lwjgl.system.MemoryStack;

import engine.Application;
import engine.data.ProjectData;
import engine.enums.Resource;
import engine.tools.maths.Maths;

public class ResourceLoader {

	public static String loadPath(String path) {
		try {
			return new File(path).getAbsolutePath();
		} catch (Exception e) {
			System.err.println("ERROR: file " + path + " cannot be found!");
			return null;
		}
	}

	public static BufferedImage loadImage(String path) {
		BufferedImage image = null;
		try {
			File f = loadFileFromEnum(Resource.IMAGES, path);
			if (f == null)
				f = loadFileFromEnum(Resource.GUI, path);
			if (f == null)
				f = loadFileFromEnum(Resource.ASSETS, path);
			if (f == null) {
				System.err.println("ERROR: image " + path + " cannot be found!");
				return image;
			}
			image = ImageIO.read(f);

		} catch (IOException e) {
			System.err.println("ERROR: image " + path + " cannot be found!");
		}
		return image;
	}
	
	/*
	 * Load an GLFW Icon from a path.
	 * 
	 * The path origin is "{projectName}/assets".
	 * This method return a GLFWImageBuffer and take a String for the path.
	 * 
	 */
	public static GLFWImage.Buffer loadIcon(String path) {
		GLFWImage image = GLFWImage.malloc();
		GLFWImage.Buffer imageBuffer = GLFWImage.malloc(1);
		
		ByteBuffer buffer;
		
		try(MemoryStack stack = MemoryStack.stackPush()){
			IntBuffer comp = stack.mallocInt(1);
			IntBuffer w = stack.mallocInt(1);
			IntBuffer h = stack.mallocInt(1);
			buffer = STBImage.stbi_load(loadFileFromEnum(Resource.ASSETS, path).getAbsolutePath(), w, h, comp, 4);
			if(buffer == null) return null;
			
			image.set(w.get(), h.get(), buffer);
			imageBuffer.put(0, image);
			
			return imageBuffer;
		}
	}

	public static int loadTexture(String path) {
		return loadTexture(loadImage(path));
	}

	public static int loadTexture(BufferedImage image) {
		if (image != null) {
			int[] pixels = new int[image.getWidth() * image.getHeight()];
			image.getRGB(0, 0, image.getWidth(), image.getHeight(), pixels, 0, image.getWidth());

			ByteBuffer buffer = BufferUtils.createByteBuffer(image.getWidth() * image.getHeight() * 4);

			for (int y = 0; y < image.getHeight(); y++) {
				for (int x = 0; x < image.getWidth(); x++) {
					int pixel = pixels[y * image.getWidth() + x];
					buffer.put((byte) ((pixel >> 16) & 0xFF)); // Red component
					buffer.put((byte) ((pixel >> 8) & 0xFF)); // Green component
					buffer.put((byte) (pixel & 0xFF)); // Blue component
					buffer.put((byte) ((pixel >> 24) & 0xFF)); // Alpha component. Only for RGBA
				}
			}

			buffer.flip();

			int textureID = GL11.glGenTextures(); // Generate texture ID
			GL11.glBindTexture(GL11.GL_TEXTURE_2D, textureID); // Bind texture ID

			GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_WRAP_S, GL12.GL_CLAMP_TO_EDGE);
			GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_WRAP_T, GL12.GL_CLAMP_TO_EDGE);

			GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MIN_FILTER, GL11.GL_NEAREST);
			GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MAG_FILTER, GL11.GL_NEAREST);

			GL11.glTexImage2D(GL11.GL_TEXTURE_2D, 0, GL11.GL_RGBA8, image.getWidth(), image.getHeight(), 0,
					GL11.GL_RGBA, GL11.GL_UNSIGNED_BYTE, buffer);

			return textureID;
		}

		System.err.println("ERROR : BufferedImage == null!");

		return 0;
	}
	
	public static Font createFont(String path, float size) {
		try {
			Font f = Font.createFont(Font.TRUETYPE_FONT, ResourceLoader.loadFileFromEnum(Resource.ASSETS, path));
			return f.deriveFont(size);
			
		} catch (FontFormatException | IOException e) {
			return null;
		}
	}

	public static File loadFileFromEnum(Resource directories, String path) {
		String projectPath = System.getProperty("user.dir") + "/";
		ProjectData.IncludePathData[] data = Application.CURRENT_APP.getApplicationData().getIncludePaths();
		File f;

		switch (directories) {
		case NORMAL:
			return new File(path);

		case ASSETS:
			for (int i = 0; i < data.length; i++) {
				f = new File(projectPath + data[i].getAssetDirectory() + "/" + path);
				if (f.exists())
					return f;
			}

			break;

		case SCRIPTS:
			for (int i = 0; i < data.length; i++) {
				f = new File(projectPath + data[i].getScriptsDirectory() + "/" + path);
				if (f.exists())
					return f;
			}

			break;

		case IMAGES:
			for (int i = 0; i < data.length; i++) {
				f = new File(projectPath + data[i].getImagesDirectory() + "/" + path);
				if (f.exists())
					return f;
			}

			break;

		case GUI:
			for (int i = 0; i < data.length; i++) {
				f = new File(projectPath + data[i].getGuiDirectory() + "/" + path);
				if (f.exists())
					return f;
			}

			break;

		case SOUNDS:
			for (int i = 0; i < data.length; i++) {
				f = new File(projectPath + data[i].getSoundDirectory() + "/" + path);
				if (f.exists())
					return f;
			}

			break;

		default:
			return new File(path);
		}

		return null;
	}

}
