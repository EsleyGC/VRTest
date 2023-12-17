# Zong Game Programmer Test
A basic VR test for the Zong Global Team's hiring process.

## Objective
The primary goal of this test was to create a VR experience centered around a main object that users can interact with. Upon interaction, various systems should be activated, including a inventory system seamlessly integrated into the user interface (UI) and a basic checkpoint mechanism.

## Systems Overview

### Checkpoint System
The checkpoint system used in this project is a simple yet scalable system that primarily functions to store checkpoints triggered by events indicating the current state of the inventory and player position. Subsequently, it awaits being called again to report the data from the last checkpoint.

In the future, the system could be employed to store additional information about various objects in the environment. For instance, if the checkpoint needed to be saved just before the player picks up the sphere, it would need to retain data about the object and the moving wall as well. This could be easily achieved by implementing an interface system on these objects that subscribes to checkpoint events, allowing it to store and utilize data passed through callbacks.

### Inventory System

The inventory system is also a simple, closed-for-modification yet open-for-extension system. In the current project, its primary function is to indicate which items the player possesses in their "bag" and how these items store information and can interact with each other through other systems such as the UI, dropboxes, and the player's direct interaction with objects in the scene.

The assembly definition of the inventory system does not need to include other systems since the inventory only needs to be accessed, not managing other systems.

The inventory system is heavily utilized by the main game UI, where the player can access which weapons and tools they possess and track modifications that occur during the step-by-step progression of the experiment.

### Notification System

The notification system utilizes modal texts to indicate status changes for practically all systems used in the experiment. It ranges from signaling that the checkpoint system has just saved the current state to displaying when an item is added or removed, and specifying the dropbox into which an item has been deposited.

Creating a UI system that mimics the overlay screen (as opposed to world space) requires careful consideration, especially given the distinct functionality in VR.

Regarding the rendering of these notifications on the screen, I cannot definitively claim to have achieved optimal results. Further exploration is needed to better understand the implementation nuances and observe how other projects successfully integrate these systems without causing users to perceive UI screenspace redundantly, ensuring it remains focused and unintrusive.

### Audio system

The audio system used in the project is as basic as possible, relying solely on audio sources (mostly in 2D) to trigger sounds at specific moments during interactions with various systems in the experiment.

### Player Handler

A basic system for handling player interactions has been added to facilitate communication and interactions with various elements. This includes an input action to invoke the UI and listen for checkpoint and inventory events, allowing the player to take actions based on these triggers.

## Result

Working on a VR project is an almost entirely different experience compared to other types of platforms, particularly when it comes to how players input commands and perceive the created universe around them. Despite this, a significant portion of the knowledge gained from the development of other multi-platform projects still applies seamlessly to VR projects.

As a VR device was not utilized in the production of this experiment, I couldn't achieve the exact experience I envisioned, but it closely aligns with my initial concept. Further exploration would result in a more tailored experience for the player, with more refined features, especially those related to how the player views the UI and manages inputs.


