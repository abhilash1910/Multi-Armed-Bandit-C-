# Multi-Armed-Bandit in Unity
Introduction

MUlti Armed Bandit is a classical Reinforcement Learning problem in which the Agent has to choose between Exploitation and Exploration in a defined Environment to maximize its rewards. The MAB problem applies a brute force approach to primarily test out all the valid rewards from different states and then either chooses between exploiting a fixed state or venturing into a new one.

Description

There are two algorithms which are at the forefront of MAB- epsilon greedy and Upper Confidence Bound. The epsilon greedy algorithm focuses on maximising its rewards based on exploiting the current resource with (1-epsilon) probability and exploring new states with epsilon probability. The UCB algorithm focuses on iterating over a number of states based on the probability distribution of rewards of each state and based on the activation function (reLU,softmax) chooses the best possible outcome of either exploring or exploiting.The source code contains a hybrid approach which combines the above 2 algorithms to provide a better use case.

Details of Project

The project file contains a C# script called 'Agent' which contains the core logic of the MAB and some initializations of game objects. A simplified bandit with 3 slots(3 cubes) with varying probabilities have been created to test whether the bandit exploits the current slot(cube) or explores a new slot(cube) based on the rewards at that particular state. The entire code is in C# and provides a template for future development work for anyone who would like to use this.
The template source code can be used as a starting point for Thompson modelling,and other optimized MAB approaches.
