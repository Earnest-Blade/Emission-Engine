#pragma once
#ifndef EMISSION_IMAGE
#define EMISSION_IMAGE

#define STB_IMAGE_IMPLEMENTATION
#include "natives/stb_image.h"

using namespace System;

namespace Emission {

	namespace Assets {
		public enum class ColorFormat : int
		{
			Default = STBI_default,
			Grey = STBI_grey,
			GretAlpha = STBI_grey_alpha,
			RedGreenBlue = STBI_rgb,
			RedGreenBlueAlpha = STBI_rgb_alpha
		};

		public ref class Image
		{
		public:
			static array<unsigned char>^ LoadImageFromPath(String^ path, int* width, int* height);
			static array<unsigned char>^ LoadImageFromPath(String^ path, int* width, int* height, ColorFormat colorFormat);
			static array<unsigned char>^ LoadImageFromPath(String^ path, int* width, int* height, int* comp, ColorFormat colorFormat);

			static array<unsigned char>^ LoadImageFromMemory(array<Byte>^ buffer, int* width, int* height);
			static array<unsigned char>^ LoadImageFromMemory(array<Byte>^ buffer, int* width, int* height, ColorFormat colorFormat);
			static array<unsigned char>^ LoadImageFromMemory(array<Byte>^ buffer, int* width, int* height, int* comp, ColorFormat colorFormat);

		private:
		
		};
	}
}

#endif

