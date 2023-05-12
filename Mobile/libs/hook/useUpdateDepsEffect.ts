import { DependencyList, EffectCallback, useEffect, useRef } from 'react'

function useUpdateDepsEffect(effect: EffectCallback, deps?: DependencyList) {
    const count = useRef(0);
    useEffect(() => {
        if (!deps) {
            return effect();
        }
        if (count.current >= deps.length) {
            return effect();
        }
        else
        {
            count.current++;
        }
    }, deps);
}

export default useUpdateDepsEffect;