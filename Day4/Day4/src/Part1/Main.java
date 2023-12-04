package Part1;

import org.jetbrains.annotations.NotNull;

import java.io.*;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Main
{
	private static class NumbersPair
	{
		public List<Integer> winningNumbers;
		public List<Integer> playerNumbers;

		public int totalPoints;
		public int matches;

		public NumbersPair()
		{
			winningNumbers = new ArrayList<>();
			playerNumbers = new ArrayList<>();

			totalPoints = 0;
			matches = 0;
		}

		public void CalculatePoints()
		{
			boolean first = true;

			int calculatePoints = 0;

			for (int playerNumber: playerNumbers)
			{
				for (int winningNumber: winningNumbers)
				{
					if (playerNumber == winningNumber)
					{
						if (first)
						{
							calculatePoints = 1;

							first = false;
						}
						else
						{
							calculatePoints *= 2;
						}

						break;
					}
				}
			}

			totalPoints = calculatePoints;
		}
	}

	public static void main(String[] args) throws IOException
	{
		List<String> rawNumberList = ReadFile();

		List<NumbersPair> numbersPairList = new ArrayList<>();

		for (String rawNumber: rawNumberList)
		{
			numbersPairList.add(ParseNumbers(rawNumber));
		}

		int total = 0;

		for (NumbersPair numbersPair: numbersPairList)
		{
			numbersPair.CalculatePoints();
			total += numbersPair.totalPoints;
		}

		System.out.println("Total points: " + total);
	}

	private static @NotNull NumbersPair ParseNumbers(String rawNumberString)
	{
		Pattern pattern = Pattern.compile("(?<=:)(?:\\s*\\d+\\s*)*(?=\\|)|(?<=\\|)(?:\\s*\\d+\\s*)*");

		Matcher matcher = pattern.matcher(rawNumberString);

		String winningNumbers = "";
		String userNumbers = "";

		int count = 0;

		while(matcher.find())
		{
			int start = matcher.start();
			int end = matcher.end();

			if (count == 0)
			{
				winningNumbers = rawNumberString.substring(start, end);
			}
			else
			{
				userNumbers = rawNumberString.substring(start, end);
			}

			count++;
		}

		NumbersPair numbersPair = new NumbersPair();

		numbersPair.winningNumbers = NumberSequenceParser(winningNumbers);
		numbersPair.playerNumbers = NumberSequenceParser(userNumbers);

		return numbersPair;
	}

	private static @NotNull List<Integer> NumberSequenceParser(String sequence)
	{
		List<Integer> returnValues = new ArrayList<>();

		Pattern pattern = Pattern.compile("\\d+");

		Matcher matcher = pattern.matcher(sequence);

		while (matcher.find())
		{
			int start = matcher.start();
			int end = matcher.end();

			String stringNumber = sequence.substring(start, end);

			returnValues.add(Integer.parseInt(stringNumber));
		}

		return returnValues;
	}

	private static @NotNull List<String> ReadFile() throws IOException
	{
		List<String> numbers = new ArrayList<>();

		String path = String.format("%s\\%s", System.getProperty("user.dir"), "out\\production\\Day4\\numbers.txt");

		System.out.println(path);

		FileReader reader = new FileReader(path);

		BufferedReader bufferedReader = new BufferedReader(reader);

		String line;
		while ((line = bufferedReader.readLine()) != null)
		{
			numbers.add(line);
		}

		bufferedReader.close();
		reader.close();

		return numbers;
	}
}