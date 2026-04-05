# Game-Core-Architecture

System Architecture Overview
A high-performance, modular 2.5D Isometric Framework developed in C# for Unity. This core is engineered for Deterministic Physics Interaction and Dynamic Entity Hierarchies, optimized specifically for Integrated GPU (Intel Iris Xe) architectures through low-overhead state management.

Implemented Core Modules (Current)
Kinematic Translation Engine: Implements Rigidbody interpolation and discrete collision detection for spatial stability on shared-memory systems.

Decoupled Interaction Logic: Standalone GrabHandler utilizing transform-parenting and kinematic-state switching to eliminate physics-engine jitter during entity-to-entity attachment.

Linear-Interpolated Isometric Projection: A LateUpdate camera system utilizing Vector3.Lerp to maintain perspective-depth synchronization.

Scalar-Based Entity Status: Initial implementation of "Respect" and "Authority" variables for future AI behavior triggering.

Architecture Roadmap (Planned Implementation)
1. Social Hierarchy & Respect System
Emergent Behavioral Logic: NPCs will utilize a "Respect-to-Aggression" ratio to determine interaction states (Alliance, Neutrality, or Combat).

Non-Linear Status Mapping: Entity "Respect" levels will dynamically shift based on environmental interactions, physical dominance, and hierarchical positioning.

2. Advanced Combat & Physics Interaction
Kinetic Force Distribution: Implementation of "Slash" and "Impact" vectors based on mass-velocity calculations during entity-to-entity collisions.

Contextual Grappling: Expanding the current GrabHandler to include directional throws and joint-based physics constraints.

3. Environmental & Performance Optimization
Asset-Culling Pipeline: Automated management of hand-drawn 2D assets to minimize VRAM footprint and draw-call overhead on hardware.

Spatial Persistence: A lightweight data-saving architecture to maintain entity status and respect levels across multiple scene loads.
