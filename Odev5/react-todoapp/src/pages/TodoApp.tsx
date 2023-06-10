import { useState } from 'react';
import { Button, Header, Input, List } from 'semantic-ui-react';

function TodoApp() {
  const [todos, setTodos] = useState<any[]>([]);
  const [newTodo, setNewTodo] = useState('');
  const [sortOrder, setSortOrder] = useState<'asc' | 'desc'>('asc');

  const addTodo = () => {
    if (newTodo.trim() !== '') {
      const todo = {
        id: todos.length + 1,
        task: newTodo,
        isCompleted: false,
        createdDate: new Date().toLocaleString(),
      };
      setTodos([...todos, todo]);
      setNewTodo('');
    }
  };

  const toggleComplete = (id: number) => {
    setTodos((prevTodos) =>
      prevTodos.map((todo: any) => {
        if (todo.id === id) {
          return {
            ...todo,
            isCompleted: !todo.isCompleted,
          };
        }
        return todo;
      })
    );
  };

  const deleteTodo = (id: number) => {
    setTodos((prevTodos) => prevTodos.filter((todo: any) => todo.id !== id));
  };

  const sortByDate = () => {
    setSortOrder((prevSortOrder) => (prevSortOrder === 'asc' ? 'desc' : 'asc'));
    setTodos((prevTodos) => {
      const sorted = [...prevTodos].sort((a: any, b: any) => {
        const aDate = new Date(a.createdDate).getTime();
        const bDate = new Date(b.createdDate).getTime();
        return sortOrder === 'asc' ? aDate - bDate : bDate - aDate;
      });
      return sorted;
    });
  };

  const sortedTodos = [...todos].sort((a: any, b: any) => {
    const aDate = new Date(a.createdDate).getTime();
    const bDate = new Date(b.createdDate).getTime();
    return sortOrder === 'asc' ? aDate - bDate : bDate - aDate;
  });

  return (
    <div className="App">
      <Header as='h2' icon='pencil alternate' content='To Do App' />
      <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
        <Input
          placeholder="Add new todo..."
          value={newTodo}
          onChange={(e) => setNewTodo(e.target.value)}
        />
        <Button color='purple' onClick={addTodo} disabled={newTodo.trim() === ''}>
          Add
        </Button>
        <Button color='olive' onClick={sortByDate}>Sort by Date</Button>
      </div>

      <List>
        {sortedTodos.map((todo: any) => (
          <List.Item
            key={todo.id}
            style={{ textDecoration: todo.isCompleted ? 'line-through' : 'none' }}
            onDoubleClick={() => toggleComplete(todo.id)}
          >
            <List.Content floated="right">
              <Button color='red' icon="trash" onClick={() => deleteTodo(todo.id)} />
            </List.Content>
            <List.Content>{todo.task}</List.Content>
          </List.Item>
        ))}
      </List>
    </div>
  );
}


export default TodoApp;
