#pragma once

#include <string.h>

#define NK_INCLUDE_STANDARD_VARARGS
#define NK_INCLUDE_FIXED_TYPES
#define NK_INCLUDE_STANDARD_IO
#define NK_INCLUDE_DEFAULT_ALLOCATOR
#define NK_INCLUDE_VERTEX_BUFFER_OUTPUT
#define NK_INCLUDE_FONT_BAKING
#define NK_INCLUDE_DEFAULT_FONT
#define NK_IMPLEMENTATION
#define NK_GLFW_GL4_IMPLEMENTATION
#define NK_KEYSTATE_BASED_INPUT
#include "natives/nuklear.h"

#ifndef NUKLEAR_GLFW_IMPL
#define NUKLEAR_GLFW_IMPL

#ifndef NK_GLFW_TEXT_MAX
#define NK_GLFW_TEXT_MAX 256
#endif
#ifndef NK_GLFW_DOUBLE_CLICK_LO
#define NK_GLFW_DOUBLE_CLICK_LO 0.02
#endif
#ifndef NK_GLFW_DOUBLE_CLICK_HI
#define NK_GLFW_DOUBLE_CLICK_HI 0.2
#endif
#ifndef NK_GLFW_MAX_TEXTURES
#define NK_GLFW_MAX_TEXTURES 256
#endif

using namespace Emission::Natives::GLFW;
using namespace Emission::Natives::GL;

namespace NuklearInternal {

    enum nk_glfw_init_state {
        NK_GLFW3_DEFAULT = 0,
        NK_GLFW3_INSTALL_CALLBACKS
    };

    struct nk_glfw_vertex {
        float position[2];
        float uv[2];
        nk_byte col[4];
    };

    struct nk_glfw_device {
        struct nk_buffer cmds;
        struct nk_draw_null_texture null;
        unsigned int vbo, vao, ebo;
        unsigned int prog;
        unsigned int vert_shdr;
        unsigned int frag_shdr;
        int attrib_pos;
        int attrib_uv;
        int attrib_col;
        int uniform_tex;
        int uniform_proj;
        int max_vertex_buffer, max_element_buffer;
        unsigned int font_tex;
    };

    static struct nk_glfw {
        long long* win;
        int width, height;
        int display_width, display_height;
        struct nk_glfw_device ogl;
        struct nk_context ctx;
        struct nk_font_atlas atlas;
        struct nk_vec2 fb_scale;
        unsigned int text[NK_GLFW_TEXT_MAX];
        int text_len;
        struct nk_vec2 scroll;
        double last_button_click;
        int is_double_click_down;
        struct nk_vec2 double_click_pos;
    } glfw;

    static struct nk_context* nk_glfw_init(long long * window, enum nk_glfw_init_state init_state, int max_vertex_buffer, int max_element_buffer);
    static void nk_glfw_shutdown(void);
    static void nk_glfw_font_stash_begin(struct nk_font_atlas** altas);
    static void nk_glfw_font_stash_end(void);
    static void nk_glfw_new_frame(void);
    static void nk_glfw_render(enum nk_anti_aliasing);

    static void nk_glfw_destroy(void);
    static void nk_glfw_create(void);

    static void nk_glfw_char_callback(long long* win, unsigned int codepoint);
    static void nk_gflw_scroll_callback(long long* win, double xoff, double yoff);
    static void nk_glfw_mouse_button_callback(long long* win, int button, int action, int mods);

    #define NK_SHADER_VERSION "#version 450 core\n"
    #define NK_SHADER_BINDLESS "#extension GL_ARB_bindless_texture : require\n"
    #define NK_SHADER_64BIT "#extension GL_ARB_gpu_shader_int64 : require\n"

    static void nk_glfw_device_upload_altas(void* image, int width, int height)
    {
        struct nk_glfw_device* dev = &glfw.ogl;
        Gl::glGenTextures(1, &dev->font_tex);
        Gl::glBindTexture(Gl::GL_TEXTURE_2D, dev->font_tex);
        Gl::glTexParameteri(Gl::GL_TEXTURE_2D, Gl::GL_TEXTURE_MIN_FILTER, Gl::GL_LINEAR);
        Gl::glTexParameteri(Gl::GL_TEXTURE_2D, Gl::GL_TEXTURE_MAG_FILTER, Gl::GL_LINEAR);
        Gl::glTexImage2D(Gl::GL_TEXTURE_2D, 0, Gl::GL_RGBA, width, height, 0, Gl::GL_RGBA, Gl::GL_UNSIGNED_BYTE, image);
    }
    
    static void nk_glfw_clipboard_paste(nk_handle user, struct nk_text_edit* edit) {
        unsigned char* text = Glfw::glfwGetClipboardString((long long*)glfw.win);
        if (text) nk_textedit_paste(edit, (const char*)text, nk_strlen((const char*)text));
        (void)user;
    }

    static void nk_glfw_clipboard_copy(nk_handle user, const char* text, int len) {
        char* str = 0;
        (void)user;
        if (!len) return;
        str = (char*)malloc((size_t)len + 1);
        if (!str) return;
        memcpy(str, text, (size_t)len);
        str[len] = '\0';
        Glfw::glfwSetClipboardString(glfw.win, (unsigned char*)str);
        free(str);
    }
}

void ::NuklearInternal::nk_glfw_create(void) {
    int status;

    static const char* vertex_shader =
        NK_SHADER_VERSION
        "layout(location = 0) in vec2 position;\n"
        "layout(location = 1) in vec2 uv;\n"
        "layout(location = 2) in vec4 color;\n"
        "layout(location = 3) uniform mat4 projection;\n"
        "out vec4 fragColor;\n"
        "out vec2 fragUv;\n"
        "void main(){\n"
        "   gl_Position = projection * vec4(position.xy, 0.0, 1.0);\n"
        "   fragColor = color;\n"
        "   fragUv = uv;\n"
        "}";

    static const char* fragment_shader =
        NK_SHADER_VERSION
        "precision mediump float;\n"
        "layout(location = 0) uniform sampler2D Texture;\n"
        "layout(location = 0) in vec4 fragColor;\n"
        "layout(location = 1) in vec2 fragUv;\n"
        "out vec4 outColor;\n"
        "void main() {\n"
        "   outColor = fragColor * texture(Texture, fragUv.st);\n"
        "}";

    struct nk_glfw_device* dev = &glfw.ogl;
    nk_buffer_init_default(&dev->cmds);
    dev->prog = Gl::glCreateProgram();
    dev->vert_shdr = Gl::glCreateShader(Gl::GL_VERTEX_SHADER);
    dev->frag_shdr = Gl::glCreateShader(Gl::GL_FRAGMENT_SHADER);
    Gl::glShaderSource(dev->vert_shdr, 1, (unsigned char**)&vertex_shader, (int*)0);
    Gl::glShaderSource(dev->frag_shdr, 1, (unsigned char**)&fragment_shader, (int*)0);
    Gl::glCompileShader(dev->vert_shdr);
    Gl::glCompileShader(dev->frag_shdr);
    Gl::glGetShaderiv(dev->vert_shdr, Gl::GL_COMPILE_STATUS, &status);

    int len = 0;
    Gl::glGetShaderiv(dev->vert_shdr, Gl::GL_INFO_LOG_LENGTH, &len);
    if (len > 1) {
        wchar_t* log = (wchar_t*)calloc((size_t)len, sizeof(char));
        Gl::glGetShaderInfoLog(dev->vert_shdr, len, (int*)0, log);
        fprintf(stderr, "[ERROR][NUKLEAR] Failed to compile shader: %s", log);
        free(log);
    }

    Gl::glGetShaderiv(dev->frag_shdr, Gl::GL_INFO_LOG_LENGTH, &len);
    if (len > 1) {
        wchar_t* log = (wchar_t*)calloc((size_t)len, sizeof(char));
        Gl::glGetShaderInfoLog(dev->frag_shdr, len, (int*)0, log);
        fprintf(stderr, "[ERROR][Nuklear] Failed to compile shader: %s", log);
        free(log);
    }

    assert(status == Gl::GL_TRUE);
    Gl::glGetShaderiv(dev->frag_shdr, Gl::GL_COMPILE_STATUS, &status);
    assert(status == Gl::GL_TRUE);
    Gl::glAttachShader(dev->prog, dev->vert_shdr);
    Gl::glAttachShader(dev->prog, dev->frag_shdr);
    Gl::glLinkProgram(dev->prog);
    Gl::glGetProgramiv(dev->prog, Gl::GL_LINK_STATUS, &status);
    assert(status == Gl::GL_TRUE);

    dev->uniform_tex = Gl::glGetUniformLocation(dev->prog, (unsigned char*)"Texture");
    dev->uniform_proj = Gl::glGetUniformLocation(dev->prog, (unsigned char*)"projection");
    dev->attrib_pos = Gl::glGetAttribLocation(dev->prog, (unsigned char*)"position");
    dev->attrib_uv = Gl::glGetAttribLocation(dev->prog, (unsigned char*)"uv");
    dev->attrib_col = Gl::glGetAttribLocation(dev->prog, (unsigned char*)"color");
    
    {
        size_t vs = sizeof(struct nk_glfw_vertex);
        size_t vp = offsetof(struct nk_glfw_vertex, position);
        size_t vt = offsetof(struct nk_glfw_vertex, uv);
        size_t vc = offsetof(struct nk_glfw_vertex, col);

        unsigned int pos = dev->attrib_pos;
        unsigned int uv = dev->attrib_uv;
        unsigned int col = dev->attrib_col;

        Gl::glCreateVertexArrays(1, &dev->vao);
        Gl::glCreateBuffers(1, &dev->vbo);
        Gl::glCreateBuffers(1, &dev->ebo);

        Gl::glBindVertexArray(dev->vao);
        Gl::glBindBuffer(Gl::GL_ARRAY_BUFFER, dev->vbo);
        Gl::glBindBuffer(Gl::GL_ELEMENT_ARRAY_BUFFER, dev->ebo);

        Gl::glEnableVertexAttribArray(dev->attrib_pos);
        Gl::glEnableVertexAttribArray(dev->attrib_uv);
        Gl::glEnableVertexAttribArray(dev->attrib_col);

        Gl::glVertexAttribPointer(dev->attrib_pos, 2, Gl::GL_FLOAT, false, vs, reinterpret_cast<void*>(vp));
        Gl::glVertexAttribPointer(dev->attrib_uv, 2, Gl::GL_FLOAT, false, vs, reinterpret_cast<void*>(vt));
        Gl::glVertexAttribPointer(dev->attrib_col, 4, Gl::GL_UNSIGNED_BYTE, true, vs, reinterpret_cast<void*>(vc));
    }

    Gl::glBindTexture(Gl::GL_TEXTURE_2D, 0);
    Gl::glBindBuffer(Gl::GL_ARRAY_BUFFER, 0);
    Gl::glBindBuffer(Gl::GL_ELEMENT_ARRAY_BUFFER, 0);
    Gl::glBindVertexArray(0);

    fprintf(stdout, "[INFO][NUKLEAR] Finished create nuklear context for GLFW!\n");
}

void NuklearInternal::nk_glfw_destroy(void) {
    struct nk_glfw_device* dev = &glfw.ogl;
    Gl::glDetachShader(dev->prog, dev->vert_shdr);
    Gl::glDetachShader(dev->prog, dev->frag_shdr);
    Gl::glDeleteShader(dev->vert_shdr);
    Gl::glDeleteShader(dev->frag_shdr);
    Gl::glDeleteProgram(dev->prog);
    Gl::glDeleteTextures(1, &dev->font_tex);
    Gl::glDeleteBuffers(1, &dev->vbo);
    Gl::glDeleteBuffers(1, &dev->ebo);
    Gl::glDeleteVertexArrays(1, &dev->vao);
    
    nk_buffer_free(&dev->cmds);
}

void NuklearInternal::nk_glfw_render(enum nk_anti_aliasing AA) {
    struct nk_glfw_device* dev = &glfw.ogl;
    struct nk_buffer vbuf, ebuf;
    float ortho[4][4] = {
        {2.0f, 0.0f, 0.0f, 0.0f},
        {0.0f,-2.0f, 0.0f, 0.0f},
        {0.0f, 0.0f,-1.0f, 0.0f},
        {-1.0f,1.0f, 0.0f, 1.0f},
    };
    ortho[0][0] /= glfw.width;
    ortho[1][1] /= glfw.height;

    Gl::glEnable(Gl::GL_BLEND);
    Gl::glBlendEquation(Gl::GL_FUNC_ADD);
    Gl::glBlendFunc(Gl::GL_SRC_ALPHA, Gl::GL_ONE_MINUS_SRC_ALPHA);
    Gl::glDisable(Gl::GL_CULL_FACE);
    Gl::glDisable(Gl::GL_DEPTH_TEST);
    Gl::glEnable(Gl::GL_SCISSOR_TEST);
    Gl::glActiveTexture(Gl::GL_TEXTURE0);

    Gl::glUseProgram(dev->prog);
    Gl::glUniform1i(dev->uniform_tex, 0);
    Gl::glUniformMatrix4fv(dev->uniform_proj, 1, Gl::GL_FALSE, &ortho[0][0]);
    Gl::glViewport(0, 0, glfw.display_width, glfw.display_height);

    {
        const struct nk_draw_command* cmd;
        nk_size  offset = 0;

        Gl::glBindVertexArray(dev->vao);
        Gl::glBindBuffer(Gl::GL_ARRAY_BUFFER, dev->vbo);
        Gl::glBindBuffer(Gl::GL_ELEMENT_ARRAY_BUFFER, dev->ebo);

        Gl::glBufferData(Gl::GL_ARRAY_BUFFER, dev->max_vertex_buffer, nullptr, Gl::GL_STREAM_DRAW);
        Gl::glBufferData(Gl::GL_ELEMENT_ARRAY_BUFFER, dev->max_element_buffer, nullptr, Gl::GL_STREAM_DRAW);

        void* vertices = Gl::glMapBuffer(Gl::GL_ARRAY_BUFFER, Gl::GL_WRITE_ONLY);
        void* elements = Gl::glMapBuffer(Gl::GL_ELEMENT_ARRAY_BUFFER, Gl::GL_WRITE_ONLY);

        {
            struct nk_convert_config config;
            static const struct nk_draw_vertex_layout_element vertex_layout[] = {
                {NK_VERTEX_POSITION, NK_FORMAT_FLOAT, NK_OFFSETOF(struct nk_glfw_vertex, position)},
                {NK_VERTEX_TEXCOORD, NK_FORMAT_FLOAT, NK_OFFSETOF(struct nk_glfw_vertex, uv)},
                {NK_VERTEX_COLOR, NK_FORMAT_R8G8B8A8, NK_OFFSETOF(struct nk_glfw_vertex, col)},
                {NK_VERTEX_LAYOUT_END}
            };

            memset(&config, 0, sizeof(config));
            config.vertex_layout = vertex_layout;
            config.vertex_size = sizeof(struct nk_glfw_vertex);
            config.vertex_alignment = NK_ALIGNOF(struct nk_glfw_vertex);
            config.null = dev->null;
            config.circle_segment_count = 22;
            config.curve_segment_count = 22;
            config.arc_segment_count = 22;
            config.global_alpha = 1.0f;
            config.shape_AA = AA;
            config.line_AA = AA;
                
            nk_buffer_init_fixed(&vbuf, vertices, (size_t)dev->max_vertex_buffer);
            nk_buffer_init_fixed(&ebuf, elements, (size_t)dev->max_element_buffer);
            nk_convert(&glfw.ctx, &dev->cmds, &vbuf, &ebuf, &config);
        }

        Gl::glUnmapBuffer(Gl::GL_ARRAY_BUFFER);
        Gl::glUnmapBuffer(Gl::GL_ELEMENT_ARRAY_BUFFER);

        nk_draw_foreach(cmd, &glfw.ctx, &dev->cmds) {
            
            if (!cmd->elem_count) continue;

            Gl::glBindTexture(Gl::GL_TEXTURE_2D, cmd->texture.id);
            Gl::glScissor(
                cmd->clip_rect.x * glfw.fb_scale.x,
                (glfw.height - (cmd->clip_rect.y + cmd->clip_rect.h)) * glfw.fb_scale.y,
                cmd->clip_rect.w * glfw.fb_scale.x,
                cmd->clip_rect.h * glfw.fb_scale.y
            );

            Gl::glDrawElements(Gl::GL_TRIANGLES, cmd->elem_count, Gl::GL_UNSIGNED_SHORT, reinterpret_cast<void*>(offset));
            offset += cmd->elem_count * sizeof(nk_draw_index);
        }
        
        nk_clear(&glfw.ctx);
        nk_buffer_clear(&dev->cmds);
    }

    Gl::glUseProgram(0);
    Gl::glBindBuffer(Gl::GL_ARRAY_BUFFER, 0);
    Gl::glBindBuffer(Gl::GL_ELEMENT_ARRAY_BUFFER, 0);
    Gl::glBindVertexArray(0);
    
    Gl::glDisable(Gl::GL_BLEND);
    Gl::glDisable(Gl::GL_SCISSOR_TEST);
    Gl::glEnable(Gl::GL_DEPTH_TEST);
}

void NuklearInternal::nk_glfw_char_callback(long long* win, unsigned int codepoint) {
    (void)win;
    if (glfw.text_len < NK_GLFW_TEXT_MAX)
        glfw.text[glfw.text_len++] = codepoint;
}

void NuklearInternal::nk_gflw_scroll_callback(long long* win, double xoff, double yoff) {
    (void)win; (void)xoff;
    glfw.scroll.x += (float)xoff;
    glfw.scroll.y += (float)yoff;
}

void NuklearInternal::nk_glfw_mouse_button_callback(long long* win, int button, int action, int mods) {
    double x, y;
    if (button != Glfw::GLFW_MOUSE_BUTTON_LEFT) return;

    Glfw::glfwGetCursorPos((long long*)win, &x, &y);

    if (action == Glfw::GLFW_PRESS) {
        double dt = Glfw::glfwGetTime() - glfw.last_button_click;
        if (dt > NK_GLFW_DOUBLE_CLICK_LO && dt < NK_GLFW_DOUBLE_CLICK_HI) {
            glfw.is_double_click_down = nk_true;
            glfw.double_click_pos = nk_vec2((float)x, (float)y);
        }
        glfw.last_button_click = Glfw::glfwGetTime();
    }
    else glfw.is_double_click_down = nk_false;
}

void NuklearInternal::nk_glfw_font_stash_begin(struct nk_font_atlas** atlas) {
    nk_font_atlas_init_default(&glfw.atlas);
    nk_font_atlas_begin(&glfw.atlas);
    *atlas = &glfw.atlas;
}

void NuklearInternal::nk_glfw_font_stash_end(void) {
    const void* image;
    int w, h;
    image = nk_font_atlas_bake(&glfw.atlas, &w, &h, NK_FONT_ATLAS_RGBA32);
    nk_glfw_device_upload_altas(const_cast<void*>((image)), w, h);
    nk_font_atlas_end(&glfw.atlas, nk_handle_id((int)glfw.ogl.font_tex), &glfw.ogl.null);
    if (glfw.atlas.default_font)
        nk_style_set_font(&glfw.ctx, &glfw.atlas.default_font->handle);
}

void NuklearInternal::nk_glfw_new_frame(void) {
    int i;
    double x, y;
    struct nk_context* ctx = &glfw.ctx;
    long long* win = (long long*)glfw.win;

    Glfw::glfwGetWindowSize(win, &glfw.width, &glfw.height);
    Glfw::glfwGetFramebufferSize(win, &glfw.display_width, &glfw.display_height);
    glfw.fb_scale.x = (float)glfw.display_width / (float)glfw.width;
    glfw.fb_scale.y = (float)glfw.display_height / (float)glfw.height;

    nk_input_begin(ctx);
    for (int i = 0; i < glfw.text_len; i++) {
        nk_input_unicode(ctx, glfw.text[i]);
    }

    nk_input_key(ctx, NK_KEY_DEL, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_DELETE) == Glfw::GLFW_PRESS);
    nk_input_key(ctx, NK_KEY_ENTER, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_ENTER) == Glfw::GLFW_PRESS);
    nk_input_key(ctx, NK_KEY_TAB, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_TAB) == Glfw::GLFW_PRESS);
    nk_input_key(ctx, NK_KEY_BACKSPACE, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_BACKSPACE) == Glfw::GLFW_PRESS);
    nk_input_key(ctx, NK_KEY_UP, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_UP) == Glfw::GLFW_PRESS);
    nk_input_key(ctx, NK_KEY_DOWN, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_DOWN) == Glfw::GLFW_PRESS);
    nk_input_key(ctx, NK_KEY_TEXT_START, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_HOME) == Glfw::GLFW_PRESS);
    nk_input_key(ctx, NK_KEY_TEXT_END, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_END) == Glfw::GLFW_PRESS);
    nk_input_key(ctx, NK_KEY_SCROLL_START, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_HOME) == Glfw::GLFW_PRESS);
    nk_input_key(ctx, NK_KEY_SCROLL_END, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_END) == Glfw::GLFW_PRESS);
    nk_input_key(ctx, NK_KEY_SCROLL_DOWN, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_PAGE_DOWN) == Glfw::GLFW_PRESS);
    nk_input_key(ctx, NK_KEY_SCROLL_UP, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_PAGE_UP) == Glfw::GLFW_PRESS);
    nk_input_key(ctx, NK_KEY_SHIFT, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_LEFT_SHIFT) == Glfw::GLFW_PRESS ||
        Glfw::glfwGetKey(win, Glfw::GLFW_KEY_RIGHT_SHIFT) == Glfw::GLFW_PRESS);

    if (Glfw::glfwGetKey(win, Glfw::GLFW_KEY_LEFT_CONTROL) == Glfw::GLFW_PRESS ||
        Glfw::glfwGetKey(win, Glfw::GLFW_KEY_RIGHT_CONTROL) == Glfw::GLFW_PRESS) {
        nk_input_key(ctx, NK_KEY_COPY, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_C) == Glfw::GLFW_PRESS);
        nk_input_key(ctx, NK_KEY_PASTE, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_V) == Glfw::GLFW_PRESS);
        nk_input_key(ctx, NK_KEY_CUT, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_X) == Glfw::GLFW_PRESS);
        nk_input_key(ctx, NK_KEY_TEXT_UNDO, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_Z) == Glfw::GLFW_PRESS);
        nk_input_key(ctx, NK_KEY_TEXT_REDO, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_R) == Glfw::GLFW_PRESS);
        nk_input_key(ctx, NK_KEY_TEXT_WORD_LEFT, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_LEFT) == Glfw::GLFW_PRESS);
        nk_input_key(ctx, NK_KEY_TEXT_WORD_RIGHT, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_RIGHT) == Glfw::GLFW_PRESS);
        nk_input_key(ctx, NK_KEY_TEXT_LINE_START, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_B) == Glfw::GLFW_PRESS);
        nk_input_key(ctx, NK_KEY_TEXT_LINE_END, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_E) == Glfw::GLFW_PRESS);
    }
    else {
        nk_input_key(ctx, NK_KEY_LEFT, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_LEFT) == Glfw::GLFW_PRESS);
        nk_input_key(ctx, NK_KEY_RIGHT, Glfw::glfwGetKey(win, Glfw::GLFW_KEY_RIGHT) == Glfw::GLFW_PRESS);
        nk_input_key(ctx, NK_KEY_COPY, 0);
        nk_input_key(ctx, NK_KEY_PASTE, 0);
        nk_input_key(ctx, NK_KEY_CUT, 0);
        nk_input_key(ctx, NK_KEY_SHIFT, 0);
    }

    Glfw::glfwGetCursorPos(win, &x, &y);
    nk_input_motion(ctx, (int)x, (int)y);
#ifdef NK_GLFW_GL4_MOUSE_GRABBING
    if (ctx->input.mouse.grabbed) {
        glfwSetCursorPos(glfw.win, ctx->input.mouse.prev.x, ctx->input.mouse.prev.y);
        ctx->input.mouse.pos.x = ctx->input.mouse.prev.x;
        ctx->input.mouse.pos.y = ctx->input.mouse.prev.y;
    }
#endif
    nk_input_button(ctx, NK_BUTTON_LEFT, (int)x, (int)y, Glfw::glfwGetMouseButton(win, Glfw::GLFW_MOUSE_BUTTON_LEFT) == Glfw::GLFW_PRESS);
    nk_input_button(ctx, NK_BUTTON_MIDDLE, (int)x, (int)y, Glfw::glfwGetMouseButton(win, Glfw::GLFW_MOUSE_BUTTON_MIDDLE) == Glfw::GLFW_PRESS);
    nk_input_button(ctx, NK_BUTTON_RIGHT, (int)x, (int)y, Glfw::glfwGetMouseButton(win, Glfw::GLFW_MOUSE_BUTTON_RIGHT) == Glfw::GLFW_PRESS);
    nk_input_button(ctx, NK_BUTTON_DOUBLE, (int)glfw.double_click_pos.x, (int)glfw.double_click_pos.y, glfw.is_double_click_down);
    nk_input_scroll(ctx, glfw.scroll);
    nk_input_end(&glfw.ctx);
    glfw.text_len = 0;
    glfw.scroll = nk_vec2(0, 0);
}

nk_context* NuklearInternal::nk_glfw_init(long long * window, nk_glfw_init_state init_state, int max_vertex_buffer, int max_element_buffer)
{
    glfw.win = window;
    if (init_state == NK_GLFW3_INSTALL_CALLBACKS) {
        // bindings
    }

    nk_init_default(&glfw.ctx, 0);
    //glfw.ctx.clip.copy = nk_glfw_clipboard_copy;
    //glfw.ctx.clip.paste = nk_glfw_clipboard_paste;
    glfw.ctx.clip.userdata = nk_handle_ptr(0);
    glfw.last_button_click = 0;

    {
        struct nk_glfw_device* dev = &glfw.ogl;
        dev->max_vertex_buffer = max_vertex_buffer;
        dev->max_element_buffer = max_element_buffer;
        nk_glfw_create();
    }

    glfw.is_double_click_down = nk_false;
    glfw.double_click_pos = nk_vec2(0, 0);

    return &glfw.ctx;
}

void NuklearInternal::nk_glfw_shutdown(void) {
    nk_font_atlas_clear(&glfw.atlas);
    nk_free(&glfw.ctx);
    nk_glfw_destroy();
    memset(&glfw, 0, sizeof(glfw));
}


#endif // !NUKLEAR_GLFW_IMPL

int main() { return 0; }