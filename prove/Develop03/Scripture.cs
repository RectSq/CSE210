using System;
using System.Collections.Generic;
using System.Linq; // Used for easy list filtering

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        //split text into words
        string[] splitText = text.Split(' ');
        foreach (string part in splitText)
        {
            _words.Add(new Word(part));
        }
    }

    // Hides a specific number of random words
    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();

        // Filter List
        // don't waste time checking words we already hid
        List<Word> visibleWords = _words.Where(word => !word.IsHidden()).ToList();

        if (visibleWords.Count <= numberToHide)
        {
            foreach (var word in visibleWords)
            {
                word.Hide();
            }
        }
        else
        {
            for (int i = 0; i < numberToHide; i++)
            {
                int index = random.Next(visibleWords.Count);
                visibleWords[index].Hide();

                visibleWords.RemoveAt(index);
            }
        }
    }

    public string GetDisplayText()
    {
        string scriptureText = "";
        foreach (Word word in _words)
        {
            scriptureText += word.GetDisplayText() + " ";
        }
        return $"{_reference.GetDisplayText()} {scriptureText.Trim()}";
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }
}