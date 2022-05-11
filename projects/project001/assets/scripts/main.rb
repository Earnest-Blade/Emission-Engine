#init_offset=5

# Call when the game is started
def begin()
    # show $main_menu

    $init.call # Create variables 
    $main.call # Start game
end

# Call when the game is ended
def end()
    
end

# Init variables
$init = Label.new {
    $main_font = Font::generate_font("PTSerif.ttf", 65)
}

# Started label
$main = Label.new{
    image1 = Image.new("main_menu_bg.png", 0)
    image1.size 1920, 1080
    image1.anchor "center"
    
    show image1
    #show image2
}
