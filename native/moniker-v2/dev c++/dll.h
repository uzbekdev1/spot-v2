#ifndef _DLL_H_
#define _DLL_H_

#include <string>
  
#ifdef __cplusplus
extern "C" {  // only need to export C interface if
              // used by C++ source code
#endif

  __declspec( dllexport ) char* hello_world();  

#ifdef __cplusplus
}
#endif

#endif