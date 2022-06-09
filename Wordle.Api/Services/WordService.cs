using Wordle.Api.Data;
using System.Linq;
using static Wordle.Api.Data.Game;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using Wordle.Api.Dtos;

namespace Wordle.Api.Services;

public class WordService
{
    private readonly AppDbContext _context;

    public WordService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Word> GetWordList(int wordsPerPage, int pageNum, string wordFilter)
    {

        if (String.IsNullOrEmpty(wordFilter))
        {
            var result = _context.Words
                .Skip((pageNum - 1) * wordsPerPage)
                .Take(wordsPerPage)
                .OrderBy(x => x.Value);
            return result;
        }
        else
        {
            var result = _context.Words
                .Where(x => x.Value.StartsWith(wordFilter))
                .Skip((pageNum - 1) * wordsPerPage)
                .Take(wordsPerPage)
                .OrderBy(x => x.Value);
            return result;
        }
    }

    public int GetTotalWordCount(string wordFilter)
    {
        var result = _context.Words.Where(x => x.Value.StartsWith(wordFilter)).Count();
        return result;
    }

    public void AddWord(string newWord)
    {
        if (newWord.Length != 5) { throw new Exception("Word must be exactly 5 letters long"); }
        //check if word is not null
        if (newWord != null)
        {
            Word? word = _context.Words.FirstOrDefault(x => x.Value == newWord);

            //see if the word does not exist on the DB
            if (word == null)
            {
                //we need to add the new word
                _context.Words.Add(new Word()
                {
                    Value = newWord,
                    Common = true
                });

                _context.SaveChanges();
            }
        }
    }

    public void ChangeWordCommon(string givenWord, bool common)
    {
        Word? word = _context.Words.FirstOrDefault(x => x.Value == givenWord);

        if (word != null)
        {
            word.Common = common;
            _context.SaveChanges();
        }
    }

    public void DeleteWord(string givenWord)
    {
        Word? word = _context.Words.FirstOrDefault(x => x.Value == givenWord);

        if (word!= null)
        {
            word.Active = false;
            _context.SaveChanges();
        }
    }
}