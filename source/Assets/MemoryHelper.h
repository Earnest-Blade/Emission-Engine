#pragma once

#ifndef EMISSION_MEM_HELPER
#define EMISSION_MEM_HELPER

static const char* str_to_char_ptr(System::String^ str) {
	return (char*)(void*)System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(str);
}

#endif // !EMISSION_MEM_HELPER
