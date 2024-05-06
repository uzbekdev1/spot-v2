// mrisk.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "mrisk.h"


#ifdef _MANAGED
#pragma managed(push, off)
#endif

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
    return TRUE;
}

#ifdef _MANAGED
#pragma managed(pop)
#endif 

MRISK_API  char*  __cdecl fnmrisk(int secret){
	const int version = 15280;
	if(secret == version){
		return "<RSAParameters><Exponent>AQAB</Exponent><Modulus>qtPmnB3ETTeujk38f/obyvB9HZldykiKMyrbm7KpEnjhxJluohS+SJTh04KV/xBwBvZS+TKv8b4AMzesB38/oDmhGia8G1RuIWvVFU5kzGKE1kmJhU8C3pBaGO5DgCZ3vDTBHdB4d5vleC+0KJmwQkAINkeGc7hzlKeNIUzC5POLeNQchDgrmyM0mLPFiiUgF6UgTy3ezaL2/Vi0UaAyfsETbfYxOwBjVudbBgQ7znKLznw27BOY9M3jZYiPPa8eUb1cT84qmGI9kPMzCtBBi1oTb2vt/9VBnazCR0/KSkekrdXHh7lbae+hLHKkpAduuE0HxwNpcQxMCR0FuTl5iQ==</Modulus></RSAParameters>";
	}

	return "";
}