# Directory Structure Convention

## 1. Feature-based Folder Structure

To increase modularity, improve potential for future expansions, and encourage encapsulation, I propose to use the feature-based folder structure. E.g.:

```
├───Assets/
│   ├───Models/
│   │   └───Mash Tun/
│   │       ├─Material.mat
│   │       ├─Behaviour.cs
│   │       └─Model.fbx
│   ├───Scenes/
└────Documentation

```

## 1.1 What is a Feature Folder Structure

A feature folder structure is a file organization system that organizes assets by placing them in folders named after the feature that the assets come together to create. For example, if we had a Beer asset, we would create a folder named "Beer" and store all assets related to that feature, such as scripts, art, animations, etc., within that folder. The aim of a feature folder structure is to make assets easily identifiable regarding their roles and to keep large projects with many features organized.

### 1.2 How and When to Create Folders and What to Store in Them

Folders should only be created when a new feature is being added. For example, if you're working on a ticket to add a new industrial tool "Hydraulic Press", you're going to create a new folder called "Hydraulic Press" under /Assets/Machinery/. Then all the scripts and assets used exclusively by the hydraulic press, will be placed inside that folder.
