#include "Image.h"
#include "MemoryHelper.h"

using namespace Emission::Assets;

array<unsigned char>^ Image::LoadImageFromPath(String^ path, int* width, int* height)
{
	int comp = 0;
	return Image::LoadImageFromPath(path, width, height, &comp, ColorFormat::Default);
}

array<unsigned char>^ Image::LoadImageFromPath(String^ path, int* width, int* height, ColorFormat colorFormat)
{
	int comp = 0;
	return Image::LoadImageFromPath(path, width, height, &comp, colorFormat);
}

array<unsigned char>^ Image::LoadImageFromPath(String^ path, int* width, int* height, int* comp, ColorFormat colorFormat)
{
	if (path == nullptr) return nullptr;

	stbi_set_flip_vertically_on_load(1);

	unsigned char* ptr = stbi_load(str_to_char_ptr(path), width, height, comp, (int)colorFormat);
	if (ptr == NULL) {
		stbi_image_free(ptr);
		return nullptr;
	}

	int c = (int)colorFormat;
	if (comp == nullptr) {
		if (colorFormat == ColorFormat::Default) {
			stbi_image_free(ptr);
			stbi__err("cannot found image format", "Cannot find image's color format.");
			return nullptr;
		}
	}
	else c = *comp;

	array<unsigned char>^ buffer = gcnew array<unsigned char>(*width * *height * c);
	//pin_ptr<unsigned char> bufptr = &buffer[0];

	//memcpy(bufptr, ptr, buffer->Length);

	System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)ptr, buffer, 0, buffer->Length);

	stbi_image_free(ptr);

	return buffer;
}

array<unsigned char>^ Image::LoadImageFromMemory(array<System::Byte>^ buffer, int* width, int* height)
{
	return Image::LoadImageFromMemory(buffer, width, height, NULL, ColorFormat::Default);
}

array<unsigned char>^ Image::LoadImageFromMemory(array<System::Byte>^ buffer, int* width, int* height, ColorFormat colorFormat)
{
	return Image::LoadImageFromMemory(buffer, width, height, NULL, colorFormat);
}

array<unsigned char>^ Image::LoadImageFromMemory(array<System::Byte>^ stream, int* width, int* height, int* comp, ColorFormat colorFormat)
{
	if (stream->Length == 0) return nullptr;

	pin_ptr<unsigned char> nativeBuffer = &stream[0];
	unsigned char* arr = nativeBuffer;

	stbi_set_flip_vertically_on_load(1);

	unsigned char* ptr = stbi_load_from_memory(arr, stream->Length, width, height, comp, (int)colorFormat);
	if (ptr == NULL) {
		stbi_image_free(ptr);
		return nullptr;
	}

	array<unsigned char>^ buffer = gcnew array<unsigned char>(*width * *height * *comp);
	System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)ptr, stream, 0, stream->Length);

	stbi_image_free(ptr);

	return buffer;
}
