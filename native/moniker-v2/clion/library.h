#ifndef MONIKER_LIBRARY_H
#define MONIKER_LIBRARY_H

#define MONIKER_EXPORTS
#ifdef MONIKER_EXPORTS
#define MONIKER_API __declspec(dllexport)
#else
#define MONIKER_API __declspec(dllimport)
#endif

extern "C" MONIKER_API const char* box_v2();

#endif //MONIKER_LIBRARY_H
