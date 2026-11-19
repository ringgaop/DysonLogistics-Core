# Dyson Logistics Core (DOTS Prototype)

This repository serves as the foundational architecture prototype for a massive-scale factory simulation game. The primary objective is to simulate **1,000,000+ moving items** across **10,000+ interconnected conveyor belts** at a stable 60 FPS using Unity DOTS (Data-Oriented Technology Stack).

## 🚀 Architecture Highlights
- **Data-Oriented Design (DOD)**: Items and belts are represented as pure structs in `NativeArray`, eliminating GC pressure and cache misses.
- **Burst Compiler + Job System**: Belt logic (movement, merging, splitting) is fully parallelized.
- **1D Parametric Positioning**: Belts are baked as 3D Splines. Items are tracked mathematically using a `Progress` float (0.0 to 1.0), avoiding heavy physics colliders.
- **Ring Buffers**: Strict O(1) complexity for item insertion/removal and handling belt stalls.
- **Spherical Grid Alignment**: (WIP) Hex-grid mapping and tangent/normal vector calculations for planetary curvature.

## 📊 Benchmark (Simulated Prototype on Ryzen 7 5800X)
*   `10,000 Belts + 100,000 Items`: ~0.8ms/frame (Main Thread)
*   `50,000 Belts + 500,000 Items`: ~4.5ms/frame (Main Thread)

## 🛠️ Status & Roadmap
- [x] ECS Belt Segment Definition & Memory Layout
- [x] Multi-threaded Item Progress Update (Burst)
- [ ] GPU Instancing Rendering Pipeline
- [ ] Spherical Planet Grid Alignment

## 🤖 Tasks targeted for Devin (AI Engineer)
To accelerate development, the following high-complexity tasks are planned to be delegated:
- [ ] Implement robust boundary checks and memory safety for the Ring Buffer logic.
- [ ] Write unit tests for Spherical Bezier intersection math (Edge cases at poles).
- [ ] Optimize the Compute Shader for `Graphics.DrawMeshInstancedIndirect` matrix updates.
