#init_offset=1

class MainScreen < DisplayScreen

    def initialize()
        super(0)

        font = Font::generate_font("PTSerif.ttf", 65)
        setDefaultFont(font)
        
        button = Button.new("sample", font, "#ffffff")
        button.on_hover {  button.setColor("#00ff00")}
        button.after_hover { button.setColor("#ffffff")}

        add(button)
        addImage("main_menu_bg.png", 0, 0, 1920, 1080)
    end

end

$main_menu = MainScreen.new()

