/*
 * @Luca Marpeau - 13 mars 2022
 * This following file is a part of JavaVisualNovel project. 
 * 
 * @desc
*/

package engine.enums;

public enum State {
	
	RUNNING(1),PAUSED(2),STOPPED(3),COMPILING(4),LOADING(5);
	
	private State(int stateIndex) { }
}
