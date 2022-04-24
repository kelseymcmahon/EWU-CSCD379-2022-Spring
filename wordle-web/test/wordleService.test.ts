import { WordsService } from '@/scripts/wordsService'

describe('Word Service', () => {
  test('Get a word', () => {
    const word = WordsService.getRandomWord()
    expect(word).not.toBeNull()
    expect(word).toHaveLength(5)
    expect(word).not.toHaveLength(4)
  })

  test('Words are private', () => {
    expect((WordsService as any).words).toBeUndefined()
  })
})

describe('Word Service', () => {
  test('Get a list of wildcard words with one wildcard ? symbol', () => {
    let word : string = "ch?ef";
    let wordMatches : string[] = WordsService.getWildCharacterWords(word);
    expect(wordMatches.length).toBe(1);
    expect(wordMatches.pop()).toBe("chief");
  })

  test('Get a list of wildcard words with multiple wildcard ? symbols', () => {
    let word : string = "ac???";
    let wordMatches : string[] = WordsService.getWildCharacterWords(word);
    expect(wordMatches.length).toBe(3);
    expect(wordMatches.pop()).toBe("actor");
    expect(wordMatches.pop()).toBe("acrid");
    expect(wordMatches.pop()).toBe("acorn");
  })

  test('Get a list of wildcard words with all wildcard ? symbols', () => {
    let word : string = "?????";
    let wordMatches : string[] = WordsService.getWildCharacterWords(word);
    expect(wordMatches.length).toBe(631);
  })
})