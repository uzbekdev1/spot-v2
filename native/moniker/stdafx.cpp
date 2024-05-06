// stdafx.cpp : source file that includes just the standard includes
// moniker.pch will be the pre-compiled header
// stdafx.obj will contain the pre-compiled type information

#include "stdafx.h"

// TODO: reference any additional headers you need in STDAFX.H
// and not in this file
char* __stdcall box(int secret)
{  
	const int version = 15280;
	if(secret == version){
		return "<RSAParameters><Exponent>AQAB</Exponent><Modulus>qtPmnB3ETTeujk38f/obyvB9HZldykiKMyrbm7KpEnjhxJluohS+SJTh04KV/xBwBvZS+TKv8b4AMzesB38/oDmhGia8G1RuIWvVFU5kzGKE1kmJhU8C3pBaGO5DgCZ3vDTBHdB4d5vleC+0KJmwQkAINkeGc7hzlKeNIUzC5POLeNQchDgrmyM0mLPFiiUgF6UgTy3ezaL2/Vi0UaAyfsETbfYxOwBjVudbBgQ7znKLznw27BOY9M3jZYiPPa8eUb1cT84qmGI9kPMzCtBBi1oTb2vt/9VBnazCR0/KSkekrdXHh7lbae+hLHKkpAduuE0HxwNpcQxMCR0FuTl5iQ==</Modulus></RSAParameters>";
	}

	return "";
}