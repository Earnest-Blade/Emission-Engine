#pragma once

#ifndef EMISSION_MEM_HELPER
#define EMISSION_MEM_HELPER

static const char* str_to_char_ptr(System::String^ str) {
	return static_cast<char*>(safe_cast<void*>(System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(str)));
}

#endif // !EMISSION_MEM_HELPER
