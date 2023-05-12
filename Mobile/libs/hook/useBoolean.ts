import { useCallback, useState } from 'react'

function useBoolean(defaultValue?: boolean): [boolean, () => void, React.Dispatch<React.SetStateAction<boolean>>, () => void, () => void] {
  const [value, setValue] = useState(defaultValue || false);

  const setTrue = useCallback(() => setValue(true), [])
  const setFalse = useCallback(() => setValue(false), [])
  const toggle = useCallback(() => setValue(x => !x), [])

  return [value, toggle, setValue, setTrue, setFalse];
}

export default useBoolean