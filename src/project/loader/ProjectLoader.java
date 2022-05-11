package project.loader;

import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.stream.Collector;
import java.util.stream.Collectors;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonParser;

import engine.Application;
import engine.data.ProjectData;

public class ProjectLoader {

	public static final String PROJECT_TEMPLATE_PATH = "assets/templates/json/project_template.json";

	public static final String[] ASSETS_DIRECTORIES =  { 
			"/assets", "/assets/scripts", "/assets/images", "/assets/gui", "/assets/sounds" 
	};

	public static Application generateProject(String name) {
		String projectPath = "projects/" + name + "/";
		File projectDirectory = new File(projectPath);

		Application app = new Application();
		Gson gson = new GsonBuilder().setPrettyPrinting().create();

		// Generate root folder
		if (!projectDirectory.exists()) {
			projectDirectory.mkdir();
			System.out.println("SUCCESS: root directory of " + name + " is successfuly created!");
		} else {
			System.out.println("SUCCESS: root directory of " + name + " is successfuly created!");
			System.out.println("INFO: root directory of " + name + " already exist!");
		}

		// Generate root's assets
		try {
			for (int i = 0; i < ASSETS_DIRECTORIES.length; i++) {
				Files.createDirectories(Paths.get(projectPath + ASSETS_DIRECTORIES[i]));
			}

			System.out.println("SUCCESS: assets directories are successfuly created!");

		} catch (IOException e) {
			System.err.println("ERROR: Error while creating project asset folder!");
		}

		// Copying, reading and writing project's data
		try {
			String dataPath = projectPath + "/" + name + "_data.json";
			if (!new File(dataPath).exists())
				Files.copy(Paths.get(PROJECT_TEMPLATE_PATH), Paths.get(dataPath));

			ProjectData data = gson.fromJson(
					Files.lines(Paths.get(dataPath)).collect(Collectors.joining(System.lineSeparator())),
					ProjectData.class);
			data.setName(name);
			data.setPath(projectPath);

			ProjectData.IncludePathData pathData = data.new IncludePathData();
			pathData.setAssetDirectory("projects/" + name + ASSETS_DIRECTORIES[0]);
			pathData.setScriptsDirectory("projects/" + name + ASSETS_DIRECTORIES[1]);
			pathData.setImagesDirectory("projects/" + name + ASSETS_DIRECTORIES[2]);
			pathData.setGuiDirectory("projects/" + name + ASSETS_DIRECTORIES[3]);
			pathData.setSoundDirectory("projects/" + name + ASSETS_DIRECTORIES[4]);

			if (data.getIncludePaths().length == 0) {
				data.addIncludePath(pathData);
			}

			Files.write(Paths.get(dataPath), gson.toJson(data).getBytes());
			
			app.setProjectData(data);

		} catch (IOException e) {
			System.err.println("ERROR: Error while copying, reading or writing project json data!");
			e.printStackTrace();
		}

		System.out.println("SUCCESS: Application for project " + name + " is created!");
		System.out.println("SUCCESS: Project Creating finished!");

		return app;
	}

}
