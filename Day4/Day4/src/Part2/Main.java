package Part2;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Main {

	public static void main(String[] args) throws IOException {
		String filePath = String.format("%s\\%s", System.getProperty("user.dir"), "out\\production\\Day4\\numbers.txt");

		List<String> puzzleInput = new ArrayList<>();

		BufferedReader reader = new BufferedReader(new FileReader(filePath));

		String line = reader.readLine();

		while(line != null)
		{
			puzzleInput.add(line.trim());

			line = reader.readLine();
		}

		reader.close();

		List<Integer> games = new ArrayList<>();

		for(int i = 0; i < puzzleInput.size(); i++)
		{
			games.add(1);
		}

		for(int i=0; i<puzzleInput.size(); i++)
		{
			String parts = puzzleInput.get(i).split(":")[1];

			String left = parts.split("\\|")[0];
			String right = parts.split("\\|")[1];

			List<Integer> leftList = Arrays.stream(left.trim().split("\\s+"))
					.map(Integer::parseInt)
					.toList();

			List<Integer> rightList = Arrays.stream(right.trim().split("\\s+"))
					.map(Integer::parseInt)
					.toList();


			for(int j=0; j < rightList.stream().filter(leftList::contains).count(); j++)
			{
				if(i + j + 1 < puzzleInput.size())
				{
					games.set(i + j + 1, games.get(i + j + 1) + games.get(i));
				}
			}
		}
		System.out.println("Card count: " + games.stream().mapToInt(Integer::intValue).sum());
	}
}