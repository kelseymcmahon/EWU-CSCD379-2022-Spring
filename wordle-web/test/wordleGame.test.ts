import { WordleGame, GameState } from '@/scripts/wordleGame'

describe('Game Test', () => {
  test('is an instance', () => {
    const game = new WordleGame('APPLE')
    expect(game).toBeTruthy()
  })
  test('Win Game', () => {
    const game = new WordleGame('APPLE')
    expect(game.state).toBe(GameState.Active)
    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('L')
    game.submitWord()
    expect(game.state).toBe(GameState.Active)

    game.currentWord.addLetter('A')
    game.currentWord.addLetter('P')
    game.currentWord.addLetter('P')
    game.currentWord.addLetter('L')
    game.currentWord.addLetter('E')
    game.submitWord()
    expect(game.state).toBe(GameState.Won)
  })

  test('Lose Game', () => {
    const game = new WordleGame('APPLE')
    expect(game.state).toBe(GameState.Active)
    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('L')
    game.submitWord()
    expect(game.state).toBe(GameState.Active)

    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('L')
    game.submitWord()
    expect(game.state).toBe(GameState.Active)

    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('L')
    game.submitWord()
    expect(game.state).toBe(GameState.Active)

    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('L')
    game.submitWord()
    expect(game.state).toBe(GameState.Active)

    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('L')
    game.submitWord()

    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('P')
    game.currentWord.addLetter('A')
    game.currentWord.addLetter('L')
    game.submitWord()
    expect(game.state).toBe(GameState.Lost)
  })
})

describe('correctChars Test', () => {
  const game = new WordleGame('DISCO')
  expect(game.state).toBe(GameState.Active)
  game.currentWord.addLetter('D')
  game.currentWord.addLetter('I')
  game.currentWord.addLetter('N')
  game.currentWord.addLetter('O')
  game.currentWord.addLetter('S')
  game.submitWord()

  console.log(game.currentWord.text)

  expect(game.correctChars.length).toBe(2)
  expect(game.correctChars.includes('D')).toBe(true)
  expect(game.correctChars.includes('I')).toBe(true)

  expect(game.wrongPlaceChars.length).toBe(2)
  expect(game.wrongPlaceChars.includes('O')).toBe(true)
  expect(game.wrongPlaceChars.includes('S')).toBe(true)

  expect(game.wrongChars.length).toBe(1)
  expect(game.wrongChars.includes('N')).toBe(true)
})

describe('getWildcardWords Test', () => {
  const game = new WordleGame('DISCO')
  game.currentWord.addLetter('D')
  game.currentWord.addLetter('I')
  game.currentWord.addLetter('S')
  game.currentWord.addLetter('?')
  game.currentWord.addLetter('O')
  game.submitWord()

  // const wordList = game.getWildcardWords()
  // for(var word in wordList) {
  //   console.log(word);
  // }
  console.log(game.currentWord.text)
  console.log("Hello World!")
  //expect(wordList.pop()).toBe('DISCO')

  // game.currentWord.addLetter('A')
  // game.currentWord.addLetter('?')
  // game.currentWord.addLetter('?')
  // game.currentWord.addLetter('?')
  // game.currentWord.addLetter('?')
  // game.submitWord()
  // expect(game.state).toBe(GameState.Active)
  // wordList = game.getWildcardWords()
  // expect(wordList[0]).toBe('ACORN')
  // expect(wordList[1]).toBe('ACRID')
  // expect(wordList[2]).toBe('ACTOR')
  // expect(wordList.length).toBe(29)
})
