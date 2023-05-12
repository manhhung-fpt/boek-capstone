import  { PropsWithChildren } from 'react'
interface Props extends PropsWithChildren<{}> {
  condition: boolean;
}
function ConditionalView(props: Props) {
  return (
    props.condition ? props.children : null
  )
}

export default ConditionalView