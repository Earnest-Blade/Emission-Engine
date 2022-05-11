#init_offet=1

module ScreenTools
    java_import "java.awt.Toolkit"

    def getScreenSize()
        Vector2f.new(
            Toolkit.getDefaultToolkit().getScreenSize().getWidth(),
            Toolkit.getDefaultToolkit().getScreenSize().getHeight()
        )
    end

    def coordsToScreen(x, y)
        Vector2f.new(x * getScreenSize.x, y * getScreenSize.y)
    end

    def screenToCoords(x, y)
        Vector2f.new(x / getScreenSize.x * 2, y / getScreenSize.y * 2)
    end
end

include ScreenTools