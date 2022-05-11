#init_offet=1

module Font
    
    def generate_font(path, size)
        ResourceLoader.createFont(path, Float(size))
    end
end

include Font


