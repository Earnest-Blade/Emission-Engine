#init_offset=2

# Main image class.
# Child class of engine.graphics.displayable.DisplayImage.
# This class is used to create image to display at screen.
# The image will be display on the layer 1.
class Image < DisplayImage

    def initialize(path, layer=0)
        super(path, layer)
    end

    def position(x, y)
        setPosition(ScreenTools::screenToCoords(x, y))
    end

    def scale(value)
        setScale(value)
    end

    def size(x, y)
        setSize(x, y)
    end

    def texture(path)
        changeTexture(path)
    end

    def anchor(anchor)
        setAnchor(Anchor.getAnchorByName(anchor))
    end

    def get_position
        coordsToScreen(getPosition().x, getPosition().y)
    end

    def get_scale
        getScale()
    end

    def get_size
        getSize()
    end

end

# Background class.
# Child class of engine.graphics.displayable.DisplayImage.
# This class is used to create backgrounds for games and display them on the screen.
# The background will be display on the layer 2.
class Background < DisplayImage

    def initialize(path)
        super(path, -1) 

        setAnchor(Anchor.getAnchorByName("center"))
        size $gui.window_width*2, $gui.window_height*2
    end

    def scale(value)
        setScale(value)
    end

    def size(x, y)
        setSize(x, y)
    end

    def get_scale
        getScale()
    end

    def get_size
        getSize()
    end
end

class Text < DisplayText

    def initialize(text, color, font)
        super(text, font, color , 1)
        
    end

    def text(x)
        setText(x)
    end

    def position(x, y)
        setPosition(ScreenTools::screenToCoords(x, y))
    end

    def scale(value)
        setScale(value)
    end

    def get_position
        coordsToScreen(getPosition().x, getPosition().y)
    end

    def get_scale
        getScale()
    end
end

class Button < DisplayButton
    def initialize(text, font, color, image="", width=0, height=0)
        super(text, font, color, image, width, height, 1)
        @@on_click_event = nil
        @@on_hover_event = nil
        @@after_hover_event = nil
    end

    # Abstract method 
    def OnClick()
        if @@on_click_event != nil 
            @@on_click_event.call
        end
    end

    # Abstract method 
    def OnHover()
        if @@on_hover_event != nil 
            @@on_hover_event.call
        end
    end

    def AfterHover()
        if @@after_hover_event != nil 
            @@after_hover_event.call
        end
    end

    def position(x, y)
        setPosition(ScreenTools::screenToCoords(x, y))
    end

    def scale(value)
        setScale(value)
    end

    def offset(xOffset, yOffset)
        setOffset(xOffset, yOffset)
    end

    def text(x)
        setText(x)
    end

    def anchor(anchor)
        setAnchor(Anchor.getAnchorByName(anchor))
    end

    def on_click(&block)
        @@on_click_event = block
    end

    def on_hover(&block)
        @@on_hover_event = block
    end

    def after_hover(&block)
        @@after_hover_event = block
    end

    def get_position
        coordsToScreen(getPosition().x, getPosition().y)
    end

    def get_scale
        getScale()
    end
end

class Rectangle < DisplayRectangle
    def initialize(width, height, color)
        super(width, height, color, false, 0)
        
    end

    def position(x, y)
        setPosition(ScreenTools::screenToCoords(x, y))
    end

    def scale(value)
        setScale(value)
    end

    def anchor(anchor)
        setAnchor(Anchor.getAnchorByName(anchor))
    end

    def get_position
        coordsToScreen(getPosition().x, getPosition().y)
    end

    def get_scale
        getScale()
    end
end

class FillRectangle < DisplayRectangle
    def initialize(width, height, color)
        super(width, height, color, true, 0)
    end

    def position(x, y)
        setPosition(ScreenTools::screenToCoords(x, y))
    end

    def scale(value)
        setScale(value)
    end

    def anchor(anchor)
        setAnchor(Anchor.getAnchorByName(anchor))
    end

    def get_position
        coordsToScreen(getPosition().x, getPosition().y)
    end

    def get_scale
        getScale()
    end
end

# Label class
# This class is used to create game action block.
# Actions that are puts in will be execute on after one. This class is used to organize game's flow.
# Start the label by calling ".call" method.
class Label
    attr_reader :block

    def initialize(&block)
        @block = block
    end

    def call
        @block.call
    end

    def exit(status=1)

    end
end

