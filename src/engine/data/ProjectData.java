package engine.data;

import com.google.gson.annotations.SerializedName;

public class ProjectData {
	
	private String name;
	private String version;
	private String path;
	
	private IncludePathData[] includePaths;
	
	public String getName() {return name;}
	public String getVersion() {return version;}
	public String getPath() {return path;}
	public IncludePathData[] getIncludePaths() {return includePaths;}
	
	public void setName(String name) {this.name = name;}
	public void setVersion(String version) {this.version = version;}
	public void setPath(String path) {this.path = path;}
	public void setIncludePaths(IncludePathData[] includePaths) {this.includePaths = includePaths;}
	
	public void addIncludePath(IncludePathData includePath) {
		if(includePaths != null) {
			IncludePathData[] data = new IncludePathData[includePaths.length + 1];
			for(int i = 0; i < includePaths.length; i++) data[i] = includePaths[i];
			data[data.length - 1] = includePath;
			
			setIncludePaths(data);
		}
		else {
			System.out.println("WARNING: Include path list ins't create! The element will be created in a new list.");
			includePaths = new IncludePathData[1];
			includePaths[0] = includePath;
		}
	}
	
	// Only use with ruby
	public void flush() {
		
	}
	
	@Override
	public String toString() {
		return "Project Data : [ name : " + name + ", version : " + version + ", path : " + path + "]";
	}
	
	public class IncludePathData {
		
		@SerializedName(value="/assets")
		private String assetDirectory;
		
		@SerializedName(value="/assets/scripts")
		private String scriptsDirectory;
		
		@SerializedName(value="/assets/images")
		private String imagesDirectory;
		

		@SerializedName(value="/assets/gui")
		private String guiDirectory;
		
		@SerializedName(value="/assets/sounds")
		private String soundDirectory;
		
		
		public IncludePathData() {}
		
		public String getAssetDirectory() {return assetDirectory;}
		public String getScriptsDirectory() {return scriptsDirectory;}
		public String getImagesDirectory() {return imagesDirectory;}
		public String getSoundDirectory() {return soundDirectory;}
		public String getGuiDirectory() {return guiDirectory;}
		
		public void setAssetDirectory(String assetDirectory) {this.assetDirectory = assetDirectory;}
		public void setScriptsDirectory(String scriptsDirectory) {this.scriptsDirectory = scriptsDirectory;}
		public void setImagesDirectory(String imagesDirectory) {this.imagesDirectory = imagesDirectory;}
		public void setSoundDirectory(String soundDirectory) {this.soundDirectory = soundDirectory;}
		public void setGuiDirectory(String guiDirectory) {this.guiDirectory = guiDirectory;}
	}
}
