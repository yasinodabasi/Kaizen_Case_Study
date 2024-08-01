using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen_Use_Case {
    public class ReceiptScanner {
        // Method to process and print the lines in the desired format
        public void ProcessAndPrintLines(List<Line> lines) {
         
            // Calculate the average Y-coordinate for each line's bounding box
            var linesWithAvgY = lines.Select(line => new {
                Line = line,
                AvgY = line.BoundingPoly.Vertices.Average(v => v.Y)
            }).ToList();

            // Sort lines by their average Y coordinates
            linesWithAvgY = linesWithAvgY.OrderBy(line => line.AvgY).ToList();

            // Group lines by their average Y coordinates
            List<List<string>> groupedLines = new List<List<string>>();
            double previousAvgY = linesWithAvgY[0].AvgY;
            List<string> currentGroup = new List<string>();

            foreach (var line in linesWithAvgY) {
                if (Math.Abs(line.AvgY - previousAvgY) > 10) // Adjust this threshold if needed
                {
                    groupedLines.Add(currentGroup);
                    currentGroup = new List<string>();
                }
                currentGroup.Add(line.Line.Description);
                previousAvgY = line.AvgY;
            }

            if (currentGroup.Count > 0) {
                groupedLines.Add(currentGroup);
            }

            // Print each grouped line
            for (int i = 0; i < groupedLines.Count; i++) {
                string concatenatedLine = string.Join(" ", groupedLines[i]);
                Console.WriteLine($"{i + 1} {concatenatedLine}");
            }
        }
    }
}
