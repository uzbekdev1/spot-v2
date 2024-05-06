
cmake_minimum_required(VERSION 3.27)

project(moniker)

set (CMAKE_C_FLAGS "-m32")

set(CMAKE_CXX_STANDARD 98)

add_library(moniker SHARED library.cpp)

