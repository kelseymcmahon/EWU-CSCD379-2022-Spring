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
    const word: string = 'ch?ef'
    const wordMatches: string[] = WordsService.getWildCharacterWords(word)
    expect(wordMatches.length).toBe(1)
    expect(wordMatches.pop()).toBe('chief')
  })

  test('Get a list of wildcard words with multiple wildcard ? symbols', () => {
    const word: string = 'ac???'
    const wordMatches: string[] = WordsService.getWildCharacterWords(word)
    expect(wordMatches.length).toBe(3)
    expect(wordMatches.pop()).toBe('actor')
    expect(wordMatches.pop()).toBe('acrid')
    expect(wordMatches.pop()).toBe('acorn')
  })

  test('Get a list of wildcard words with all wildcard ? symbols', () => {
    const word: string = '?????'
    const wordMatches: string[] = WordsService.getWildCharacterWords(word)
    expect(wordMatches.length).toBe(631)
  })
})

describe('wordExists Test', () => {
  test('check if given word exists on words list', () => {
    let word = 'acorn'
    expect(WordsService.wordExists(word)).toBe(true)
    word = 'hello'
    expect(WordsService.wordExists(word)).toBe(false)
  })
})
