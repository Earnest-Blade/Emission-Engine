package engine.tools;

import java.awt.Graphics2D;
import java.awt.Image;
import java.awt.image.BufferedImage;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * @autor 
 * Luca Marpeau - 18 avr. 2022
 * This following file is a part of Java VN Engine project. 
 * 
 * @description
*/
public final class Toolkit {
	
	/**
	 * This method simply resize a buffered image with a new width and a new height.
	 * It's return the new sized image.
	 */
	public static BufferedImage resizeBufferedImage(BufferedImage image, int newWidth, int newHeight) {
		Image tmp = image.getScaledInstance(newWidth, newHeight, Image.SCALE_SMOOTH);
	    BufferedImage dimg = new BufferedImage(newWidth, newHeight, BufferedImage.TYPE_INT_ARGB);

	    Graphics2D g2d = dimg.createGraphics();
	    g2d.drawImage(tmp, 0, 0, null);
	    g2d.dispose();

	    return dimg; 
	}
	
	/**
	 * This method return all digit that contain a String.
	 * return an int.
	 */
	public static int getAllDigits(String s) {
		Pattern p = Pattern.compile("\\d+");
        Matcher m = p.matcher(s);
        
        while(m.find()) {
            return Integer.parseInt(m.group());
        }
        return 0;
	}

}
